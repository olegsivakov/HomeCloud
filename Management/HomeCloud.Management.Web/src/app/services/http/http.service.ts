import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpResponse } from '@angular/common/http/src/response';
import { Observable } from 'rxjs/Observable';

import { IResource } from '../../models/http/resource';
import { ResourceArray } from '../../models/http/resource-array';
import { PagedArray } from '../../models/paged-array';

import { Relation } from '../../models/http/relation';
import { RelationArray } from '../../models/http/relation-array';
import { HttpMethod } from '../../models/http/http-method';

import 'rxjs/add/observable/throw';

@Injectable()
export class HttpService<TRelations extends RelationArray, T extends IResource<TRelations>> {

  protected resourceArray: ResourceArray<T> = new ResourceArray<T>();

  constructor(
    protected httpClient: HttpClient,
    protected resourceUrl: string) { }

  public list(limit: number): Observable<PagedArray<T>> {
    let relation: Relation = new Relation();
    relation.href = this.resourceUrl + "?limit=" + limit;
    relation.type = "application/json";
    relation.method = HttpMethod.get;
  
    return this.request<ResourceArray<T>>(relation).map(response => {
      return this.map(response);
    });
  }

  public hasNext(): boolean {
    return this.resourceArray.hasNext();
  }

  public next(): Observable<PagedArray<T>> {
    return this.request<ResourceArray<T>>(this.resourceArray._links.next).map(response => {
      return this.map(response);
    });
  }

  public hasPrevious(): boolean {
    return this.resourceArray.hasPrevious();
  }

  public previous(): Observable<PagedArray<T>> {
    return this.request<ResourceArray<T>>(this.resourceArray._links.previous).map(response => {
      return this.map(response);
    });
  }

  public hasCreate(): boolean {
    return this.resourceArray.hasCreate();
  }

  public create(entity: T): Observable<T> {
    return this.request<T>(this.resourceArray._links.create, entity).map(response => {
      return response.body;
    });
  }

  public hasItem(index: number): boolean {
    return this.resourceArray.hasItem(index);
  }

  public item(index: number): Observable<T> {
    if (this.hasItem(index)) {
      return this.request<T>(this.resourceArray._links.items[index], index).map(response => {
        return response.body;
      });
    }

    let error = new Error("No item with index " + index + " found in resource array.");
    return Observable.throw(error);
  }

  public get<TResult extends T>(entity: T): Observable<TResult> {
    return this.request<TResult>(entity._links.get).map(response => {
      return response.body;
    });
  }

  public update(entity: T): Observable<T> {
    return this.request<T>(entity._links.update, entity).map(response => {
      return response.body;
    });
  }

  public delete(entity: T): Observable<T> {
    return this.request<T>(entity._links.delete, entity).map(response => {
      return response.body;
    });
  }

  public exists(entity: T): Observable<boolean> {
    return this.request<T>(entity._links.exists, entity).map(response => {
      return response.ok;
    });
  }

  public relation<TRelation>(relation: Relation): Observable<TRelation> {
    return this.request<TRelation>(relation).map(response => {
      return response.body;
    });
  }

  public relations<TRelation>(relation: Relation): Observable<ResourceArray<TRelation>> {
    const totalCountHeader: string = "X-Total-Count";

    return this.request<ResourceArray<TRelation>>(relation).map(response => {
      response.body.items.totalCount = response.headers[totalCountHeader];

      return response.body;
    });
  }
  
  protected map(response: ResourceArray<T> | HttpResponse<ResourceArray<T>>): PagedArray<T> {
    const totalCountHeader: string = "X-Total-Count";

    if (response instanceof ResourceArray) {
      Object.assign(this.resourceArray, response);

      return response.items;
    }
    else {
      response.body.items.totalCount = response.headers[totalCountHeader];
      Object.assign(this.resourceArray, response.body);

      return response.body.items;
    }
    
  }

  private request<TResult>(relation: Relation, data?: any): Observable<HttpResponse<TResult>> {
    if (!relation) {
      return Observable.throw(new Error("Resource link is undefined"));
    }

    switch (relation.method) {
      case HttpMethod.get: {
        return this.httpClient.get<TResult>(relation.href, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        });
      }

      case HttpMethod.post: {        
        return this.httpClient.post<TResult>(relation.href, data, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        });
      }

      case HttpMethod.put: {
        
        return this.httpClient.put<TResult>(relation.href, data, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        });
      }

      case HttpMethod.delete: {
        
        return this.httpClient.delete<TResult>(relation.href, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        });
      }

      case HttpMethod.head: {
        
        return this.httpClient.head<TResult>(relation.href, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        });
      }

      default: {
        return Observable.throw(new Error("HTTP method is undefined"));
      }
    }
  }
}
