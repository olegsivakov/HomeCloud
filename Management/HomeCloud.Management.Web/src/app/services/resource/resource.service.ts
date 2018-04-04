import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { HttpMethod } from '../../models/http/http-method';
import { Relation } from '../../models/http/relation';
import { RelationArray } from '../../models/http/relation-array';
import { IResource } from '../../models/http/resource';
import { ResourceArray } from '../../models/http/resource-array';
import { PagedArray } from '../../models/paged-array';

import { CloneableService } from '../cloneable/cloneable.service';

import 'rxjs/add/observable/throw';

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
              });
      }

      case HttpMethod.post: {

        return this.httpClient.post<T>(relation.href, data, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        }).map(response => {
          return this.processResponse<T>(initializer, response);
        });
      }

      case HttpMethod.put: {

        return this.httpClient.put<T>(relation.href, data, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        }).map(response => {
          return this.processResponse<T>(initializer, response);
        });
      }

      case HttpMethod.delete: {

        return this.httpClient.delete<T>(relation.href, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        }).map(response => {
          return this.processResponse<T>(initializer, response);
        });
      }

      case HttpMethod.head: {

        return this.httpClient.head<T>(relation.href, {
          headers: new HttpHeaders({"Content-Type": relation.type}),
          observe: "response"
        }).map(response => {
          return this.processResponse<T>(initializer, response);
        });
      }

      default: {
        return Observable.throw(new Error("HTTP method is undefined"));
      }
    }
  }

  private processResponse<T extends IResource>(initializer: new() => T, response: HttpResponse<any>): IResource {
    let result: ResourceArray<T> = null;

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

      return result;
    }
    else {
      return this.cloneableService.clone<T>(initializer, response.body);
    }
  }

  private getResourceArray<T>(typeInitializer: new () => T, source: any): ResourceArray<T> {
    let result: ResourceArray<T> = new ResourceArray<T>();
    result.items = source._links ? this.cloneableService.cloneArray<T, PagedArray<T>>(PagedArray, typeInitializer, source.items) : result.items;
    result._links = source._links ? this.cloneableService.clone<RelationArray>(RelationArray, source._links) : result._links;
    result._links.items = result._links.items ? this.cloneableService.cloneArray<Relation, Array<Relation>>(Array, Relation, result._links.items) : result._links.items;


    return result;
  }
}
