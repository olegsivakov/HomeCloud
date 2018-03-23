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

@Injectable()
export class HttpService<TRelations extends RelationArray, T extends IResource<TRelations>> {

  protected resourceArray: ResourceArray<T> = new ResourceArray<T>();

  constructor(
    protected httpClient: HttpClient,
    protected resourceUrl: string) { }

  public list(limit: number): Observable<PagedArray<T>> {
    return this.httpClient.get<ResourceArray<T>>(this.resourceUrl + "?limit=" + limit, {
        headers: new HttpHeaders({"Content-Type": "application/json"})
      })
      .map(response => {
        Object.assign(this.resourceArray, response);

        return response.items;
      });
  }

  public hasNext(): boolean {
    return this.resourceArray.hasNext();
  }

  public next(): Observable<PagedArray<T>> {
    return this.request<ResourceArray<T>>(this.resourceArray._links.next)
    .map(response => {
      this.resourceArray.items = response.items;
        Object.assign(this.resourceArray, response);

        return response.items;
  });;
  }

  public hasPrevious(): boolean {
    return this.resourceArray.hasPrevious();
  }

  public previous(): Observable<PagedArray<T>> {
    return this.request<ResourceArray<T>>(this.resourceArray._links.previous)
      .map(response => {
        Object.assign(this.resourceArray, response);

        return response.items;
    });
  }  

  public hasCreate(): boolean {
    return this.resourceArray.hasCreate();
  }

  public create(entity: T): Observable<T> {
    return this.request(this.resourceArray._links.create, entity);
  }

  public hasItem(index: number): boolean {
    return this.resourceArray.hasItem(index);
  }

  public item(index: number): Observable<T> {
    return this.request(this.resourceArray._links.items[index], index);
  }

  public get<TResult extends T>(entity: T): Observable<TResult> {
    return this.request(entity._links.get);
  }

  public update(entity: T): Observable<T> {
    return this.request(entity._links.update, entity);
  }

  public delete(entity: T): Observable<T> {
    return this.request(entity._links.delete, entity);
  }

  public exists(entity: T): Observable<boolean> {
    return this.request(entity._links.exists, entity);
  }

  public request<T>(relation: Relation, data?: any): Observable<T> {
    if (!relation) {
      const error: Error = new Error("Resource link is undefined");
      console.log(error.message);

      return Observable.throw(error);
    }

    switch (relation.method) {
      case HttpMethod.get: {

      const totalCountHeader: string = "X-Total-Count";

        return this.httpClient.get(relation.href).map((response: HttpResponse<T>) => {
          if (response.body instanceof ResourceArray) {
            response.body.items.totalCount = response.headers[totalCountHeader];
          }
          else if (response.body instanceof PagedArray) {
            response.body.totalCount = response.headers[totalCountHeader];
          }

          return response.body;
        })
      }

      case HttpMethod.post: {
        
        return this.httpClient.post<T>(relation.href, {
          body: data
        });
      }

      case HttpMethod.put: {
        
        return this.httpClient.put<T>(relation.href, {
          body: data
        });
      }

      case HttpMethod.delete: {
        
        return this.httpClient.delete<T>(relation.href);
      }

      case HttpMethod.head: {
        
        return this.httpClient.head<T>(relation.href);
      }

      default: {
        const error: Error = new Error("HTTP method is undefined");
        console.log(error);

        return Observable.throw(error.message);
      }
    }
  }
}
