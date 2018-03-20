import { Injectable } from '@angular/core';
import { HttpResponse } from '@angular/common/http/src/response';
import { Observable } from 'rxjs/Observable';

import { Resource, IResource } from '../../models/http/resource';
import { ResourceArray } from '../../models/http/resource-array';
import { PagedArray } from '../../models/paged-array';

import { ResourceService } from '../resource/resource.service';

@Injectable()
export class HttpService<T extends Resource> {

  public resourceArray: ResourceArray<T> = new ResourceArray<T>();

  constructor(
    protected resourceService: ResourceService,
    private resourceUrl: string) { }

  public list(limit: number): Observable<PagedArray<T>> {
    return this.resourceService.list<T>(this.resourceUrl, limit)
      .map(response => {
        this.resourceArray = response;

        return response.items;
      });
  }

  public hasNext(): boolean {
    return this.resourceArray.hasNext();
  }

  public next(): Observable<PagedArray<T>> {
    return this.resourceService.next(this.resourceArray)
      .map(response => {
        this.resourceArray = response;

        return response.items;
    });
  }

  public hasPrevious(): boolean {
    return this.resourceArray.hasPrevious();
  }

  public previous(): Observable<PagedArray<T>> {
    return this.resourceService.previous(this.resourceArray)
      .map(response => {
        this.resourceArray = response;

        return response.items;
    });
  }

  public get<TResult extends T>(entity: T): Observable<TResult> {
    return this.resourceService.get(entity);
  }

  public relation<T extends IResource>(entity: T, relation: string): Observable<T> {
    return this.resourceService.getRelation(entity, relation);
  }

  public create(entity: T): Observable<T> {
    return this.resourceService.create(this.resourceArray, entity)
      .map((response: T) => response);
  }

  public update(entity: T): Observable<T> {
    return this.resourceService.update(entity)
      .map((response: T) => response);
  }

  public delete(entity: T): Observable<Object> {
    return this.resourceService.delete(entity);
  }

  public exists(entity: T): Observable<boolean> {
    return this.resourceService.exists(entity);
  }
}
