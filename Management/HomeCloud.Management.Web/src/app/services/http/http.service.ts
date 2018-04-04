import { Injectable } from '@angular/core';
import { HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { IResource } from '../../models/http/resource';
import { ResourceArray } from '../../models/http/resource-array';
import { Relation } from '../../models/http/relation';
import { HttpMethod } from '../../models/http/http-method';
import { PagedArray } from '../../models/paged-array';

import { ResourceService } from '../resource/resource.service';

@Injectable()
export class HttpService<T extends IResource> {

  protected resources: ResourceArray<T> = new ResourceArray<T>();

  constructor(
    protected type: new() => T,
    protected resourceService: ResourceService,
    protected resourceUrl?: string) {
    }

  public load(resources: ResourceArray<T>): void {
    this.resources = resources;
  }

  public list(limit: number): Observable<PagedArray<T>>;
  public list(limit: number | any): Observable<PagedArray<T>> {
    let relation: Relation = new Relation();
    relation.href = this.resourceUrl + "?limit=" + limit;
    relation.type = "application/json";
    relation.method = HttpMethod.get;

    return this.resourceService.request<T>(this.type, relation).map(resource => {
      return this.map(resource);
    });
  }

  public hasNext(): boolean {
    return this.resources.hasNext();
  }

  public next(): Observable<PagedArray<T>> {
    return this.resourceService.request<T>(this.type, this.resources._links.next).map(resource => {
      return this.map(resource);
    });
  }

  public hasPrevious(): boolean {
    return this.resources.hasPrevious();
  }

  public previous(): Observable<PagedArray<T>> {
    return this.resourceService.request<T>(this.type, this.resources._links.previous).map(resource => {
      return this.map(resource);
    });
  }

  public hasCreate(): boolean {
    return this.resources.hasCreate();
  }

  public create(entity: T): Observable<T> {
    return this.resourceService.request<T>(this.type, this.resources._links.create, entity).map(resource => {
      return resource as T;
    });
  }

  public hasItem(index: number): boolean {
    return this.resources.hasItem(index);
  }

  public item(index: number): Observable<T> {
    if (this.hasItem(index)) {
      return this.resourceService.request<T>(this.type, this.resources._links.items[index], index).map(resource => {
        return resource as T;
      });
    }

    let error = new Error("No item with index " + index + " found in resource array.");
    return Observable.throw(error);
  }

  public get<TResult extends T>(entity: T): Observable<TResult> {
    let resultType: new() => TResult;

    return this.resourceService.request<TResult>(resultType, entity._links.get).map(resource => {
      return resource as TResult;
    });
  }

  public update(entity: T): Observable<T> {
    return this.resourceService.request<T>(this.type, entity._links.update, entity).map(resource => {
      return resource as T;
    });
  }

  public delete(entity: T): Observable<T> {
    return this.resourceService.request<T>(this.type, entity._links.delete, entity).map(resource => {
      return resource as T;
    });
  }

  public exists(entity: T): Observable<boolean> {
    return this.resourceService.request<T>(this.type, entity._links.exists, entity).map(resource => {
      return true;
    });
  }

  public relation<TRelation extends IResource>(initializer: new() => TRelation, relation: Relation): Observable<IResource>;
  public relation<TRelation extends IResource>(initializer: new() => TRelation, relation: string, resource?: IResource): Observable<IResource>;
  public relation<TRelation extends IResource>(initializer: new() => TRelation, relation: string | Relation, resource?: IResource): Observable<IResource> {

    if (relation instanceof String) {
      return this.resourceService.request<TRelation>(initializer, (resource ? resource : this.resources)._links[relation]).map(resource => {
        return resource;
      });
    }

    return this.resourceService.request<TRelation>(initializer, relation).map(resource => {
      return resource;
    });
  }

  protected map<T>(resource: IResource): PagedArray<T> {
    if (resource instanceof ResourceArray) {
      this.load(resource);
      return resource.items;
    }

    return new PagedArray<T>();
  }
}
