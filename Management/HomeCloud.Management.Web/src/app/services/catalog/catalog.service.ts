import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ISubscription } from 'rxjs/Subscription';

import { PagedArray } from '../../models/paged-array';
import { ResourceArray } from '../../models/http/resource-array';
import { Catalog } from '../../models/catalog';
import { CatalogRelation } from '../../models/catalog-relation';
import { StorageData } from '../../models/storage-data';

import { HttpService } from '../http/http.service';
import { ResourceService } from '../resource/resource.service';
import { CatalogStateService } from '../catalog-state/catalog-state.service';

import 'rxjs/add/observable/throw';

@Injectable()
export class CatalogService extends HttpService<Catalog> {

  private catalog: Catalog = null;
  private subscription: ISubscription = null;

  constructor(
    protected resourceService: ResourceService,
    private catalogStateService: CatalogStateService) {
      super(Catalog, resourceService);

      this.subscription = this.catalogStateService.catalogChanged$.subscribe(catalog => {
        if (this.catalog && catalog && this.catalog.id != catalog.id) {
          this.resources = new ResourceArray<Catalog>();
        }

        this.catalog = catalog;
      });
  }

  public list(limit?: number): Observable<PagedArray<Catalog>>;
  public list(limit?: number): Observable<PagedArray<StorageData>> {
    let relation = (this.catalog._links as CatalogRelation).data;
    if (relation) {
      return this.relation<StorageData>(StorageData, relation).map(data => {
        let items: PagedArray<StorageData> = this.map(data);
        this.catalog.count = items.totalCount;

        return items;
      });
    }

    return Observable.throw("No resource found to list data.");
  }

  public previous(): Observable<PagedArray<Catalog>>;
  public previous(): Observable<PagedArray<StorageData>> {
    return this.relation<StorageData>(StorageData, this.resources._links.previous).map(data => {
      let items: PagedArray<StorageData> = this.map(data);
      this.catalog.count = items.totalCount;

      return items;
    });
  }

  public next(): Observable<PagedArray<Catalog>>;
  public next(): Observable<PagedArray<StorageData>> {
    return this.relation<StorageData>(StorageData, this.resources._links.next).map(data => {
      let items: PagedArray<StorageData> = this.map(data);
      this.catalog.count = items.totalCount;

      return items;
    });
  }

  public hasValidate() {
    return this.catalog.hasValidate();
  }

  public validate(entity: Catalog): Observable<Catalog> {
    entity._links.validate = this.catalog._links.validate;

    return super.validate(entity);
  }

  public delete(entity: Catalog): Observable<Catalog> {
    return super.delete(entity as Catalog).map(resource =>{
      this.catalog.count -= 1;
      this.catalogStateService.onCatalogChanged(this.catalog);

      return resource;
    });
  }

  public hasCreate(): boolean {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (!relations) {
      return false;
    }

    return relations.createCatalog && !relations.createCatalog.isEmpty();
  }

  public create(catalog: Catalog): Observable<Catalog> {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (relations) {
      return this.relation<Catalog>(Catalog, relations.createCatalog, catalog).map((resource: Catalog) => {
        this.catalog.count += 1;
        this.catalogStateService.onCatalogChanged(this.catalog);

        return resource;
      });
    }

    return Observable.throw("No resource found to create catalog");
  }
}
