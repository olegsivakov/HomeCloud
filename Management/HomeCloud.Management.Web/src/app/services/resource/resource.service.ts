import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { HttpMethod } from '../../models/http/http-method';
import { Relation } from '../../models/http/relation';
import { RelationArray } from '../../models/http/relation-array';
import { IResource, GUID } from '../../models/http/resource';
import { ResourceArray } from '../../models/http/resource-array';
import { PagedArray } from '../../models/paged-array';

import { CloneableService } from '../cloneable/cloneable.service';

import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import { HttpErrorResponse } from '@angular/common/http/src/response';
import { HttpError } from '../../models/http/http-error';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';

@Injectable()
export class ResourceService {

  constructor(
    private httpClient: HttpClient,
    private cloneableService: CloneableService) { }

  public request<T extends IResource>(initializer: new() => T, relation: Relation, data?: any): Observable<IResource> {
    if (!relation) {
      return Observable.throw(new Error("Resource link is undefined"));
    }

    switch (relation.method) {

      case HttpMethod.get: {

        return this.httpClient.get<T>(relation.href, {
                headers: new HttpHeaders({"Content-Type": relation.type}),
                observe: "response"
              }).map(response => {
                return this.processResponse<T>(initializer, response);
              }).catch((response: HttpErrorResponse) => {
                return this.processErrorResponse(response);
              });
      }

      case HttpMethod.post: {

        return this.httpClient.post<T>(relation.href, data, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        }).map(response => {
          return this.processResponse<T>(initializer, response);
        }).catch((response: HttpErrorResponse) => {
          return this.processErrorResponse(response);
        });
      }

      case HttpMethod.put: {

        return this.httpClient.put<T>(relation.href, data, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        }).map(response => {
          return this.processResponse<T>(initializer, response);
        }).catch((response: HttpErrorResponse) => {
          return this.processErrorResponse(response);
        });
      }

      case HttpMethod.delete: {

        return this.httpClient.delete<T>(relation.href, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        }).map(response => {
          return this.processResponse<T>(initializer, response);
        }).catch((response: HttpErrorResponse) => {
          return this.processErrorResponse(response);
        });
      }

      case HttpMethod.head: {

        return this.httpClient.head<T>(relation.href, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        }).map(response => {
          return this.processResponse<T>(initializer, response);
        }).catch((response: HttpErrorResponse) => {
          return this.processErrorResponse(response);
        });
      }

      default: {
        return Observable.throw(new Error("HTTP method is undefined"));
      }
    }
  }

  private processResponse<T extends IResource>(initializer: new() => T, response: HttpResponse<any>): IResource {
    let result: ResourceArray<T> = null;
    if (!response.body || response.status == 204 || response.status == 404) {
      return result;
    }

    if (response.body instanceof Array) {
      result = this.getResourceArray<T>(initializer, { items: response.body });
    }
    else if (response.body.items && response.body.items instanceof Array) {
      result = this.getResourceArray<T>(initializer, response.body);
    }

    if (result) {
      const totalCountHeader: string = "X-Total-Count";
      let totalCount: number = response.headers[totalCountHeader];      
      result.items.totalCount = totalCount ? totalCount : 0;

      if (response.body._links && response.body._links.items instanceof Array) {
        response.body._links.items.forEach((item, index) => {
          if (index < result.items.length) {
            result.items[index]._links.get = this.cloneableService.clone<Relation>(Relation, response.body._links.items[index]);
          }
        });
      }

      return result;
    }
    else {
      return this.cloneableService.clone<T>(initializer, response.body);
    }
  }

  private processErrorResponse(response: HttpErrorResponse): ErrorObservable {
    let result: HttpError = this.cloneableService.clone<HttpError>(HttpError, response.error);
    if (result.id == GUID) {
      result.statusCode = response.status;
      result.messages.push(response.message);
    }

    return Observable.throw(result);
  }

  private getResourceArray<T>(typeInitializer: new () => T, source: any): ResourceArray<T> {
    let result: ResourceArray<T> = new ResourceArray<T>();
    result.items = source._links ? this.cloneableService.cloneArray<T, PagedArray<T>>(PagedArray, typeInitializer, source.items) : result.items;
    result._links = source._links ? this.cloneableService.clone<RelationArray>(RelationArray, source._links) : result._links;
    result._links.items = result._links.items ? this.cloneableService.cloneArray<Relation, Array<Relation>>(Array, Relation, result._links.items) : result._links.items;


    return result;
  }
}
