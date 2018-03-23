import { Injectable } from '@angular/core';
import { HttpResponse, HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';

import { HttpMethod } from '../../models/http/http-method';
import { Relation } from '../../models/http/relation';
import { IResource } from '../../models/http/resource';
import { ResourceArray } from '../../models/http/resource-array';
import { PagedArray } from '../../models/paged-array';

@Injectable()
export class ResourceService {

  constructor(
    private httpClient: HttpClient)
  { }

  public list<T extends IResource>(resourceUrl: string, limit: number): Observable<ResourceArray<T>> {
    return this.httpClient.get<ResourceArray<T>>(resourceUrl + "?limit=" + limit);
  }

  public previous<T extends IResource>(resource: IResource): Observable<ResourceArray<T>> {
    return this.request(resource.relations.previous);
  }

  public next<T extends IResource>(resource: IResource): Observable<ResourceArray<T>> {
    return this.request(resource.relations.next);
  }

  public get<T extends IResource>(resource: IResource): Observable<T> {
    return this.request(resource.relations.get);
  }

  public item<T extends IResource>(resource: IResource, index: number): Observable<T> {
    return this.request(resource.relations.items[index]);
  }

  public create<T extends IResource>(resource: IResource, data: T): Observable<T> {
    return this.request(resource.relations.create);
  }

  public update<T extends IResource>(resource: T): Observable<T> {
    return this.request(resource.relations.update);
  }

  public delete<T extends IResource>(resource: T): Observable<T> {
    return this.request(resource.relations.delete)
          .map(response => resource);
  }

  public exists<T extends IResource>(resource: T): Observable<boolean> {
    return this.request(resource.relations.exists)
          .map((response: Response) => response.ok);
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
