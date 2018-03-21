import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { Link } from '../../models/http/link';
import { HttpMethod } from '../../models/http/http-method';
import { Resource, IResource } from '../../models/http/resource';
import { ResourceArray } from '../../models/http/resource-array';
import { HttpResponse, HttpClient } from '@angular/common/http';
import { PagedArray } from '../../models/paged-array';

import 'rxjs/add/observable/throw';

@Injectable()
export class ResourceService {

  constructor(
    private httpClient: HttpClient)
  { }

  public list<T extends IResource>(resourceUrl: string, limit: number): Observable<ResourceArray<T>> {
    return this.httpClient.get<ResourceArray<T>>(resourceUrl + "?limit=" + limit);
  }

  public previous<T extends IResource>(resource: ResourceArray<T>): Observable<ResourceArray<T>> {
    return this.request(resource.previous);
  }

  public next<T extends IResource>(resource: ResourceArray<T>): Observable<ResourceArray<T>> {
    return this.request(resource.next);
  }

  public get<T extends Resource>(resource: Resource): Observable<T> {
    return this.request(resource.get);
  }

  public relation<T extends IResource>(resource: IResource, relation: string, data?: any): Observable<T> {
    return this.request(resource.relation(relation), data);
  }

  public create<T extends IResource>(resource: Resource | ResourceArray<T>, data: T): Observable<T> {
    return this.request(resource.create);
  }

  public update<T extends Resource>(resource: T): Observable<T> {
    return this.request(resource.update);
  }

  public delete<T extends Resource>(resource: T): Observable<T> {
    return this.request(resource.delete)
          .map(response => resource);
  }

  public exists<T extends Resource>(resource: T): Observable<boolean> {
    return this.request(resource.exist)
          .map((response: Response) => response.ok);
  }

  public request<T>(link: Link, data?: any): Observable<T> {
    if (!link) {
      const error: Error = new Error("Resource link is undefined");
      console.log(error.message);

      return Observable.throw(error);
    }

    switch (link.method) {
      case HttpMethod.get: {

      const totalCountHeader: string = "X-Total-Count";

        return this.httpClient.get(link.href).map((response: HttpResponse<T>) => {
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
        
        return this.httpClient.post<T>(link.href, {
          body: data
        });
      }

      case HttpMethod.put: {
        
        return this.httpClient.put<T>(link.href, {
          body: data
        });
      }

      case HttpMethod.delete: {
        
        return this.httpClient.delete<T>(link.href);
      }

      case HttpMethod.head: {
        
        return this.httpClient.head<T>(link.href);
      }

      default: {
        const error: Error = new Error("HTTP method is undefined");
        console.log(error);

        return Observable.throw(error.message);
      }
    }
  }
}
