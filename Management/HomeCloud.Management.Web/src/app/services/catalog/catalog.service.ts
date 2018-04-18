import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

import { PagedArray } from '../../models/paged-array';
import { Catalog } from '../../models/catalog';
import { StorageData } from '../../models/storage-data';

import { HttpService } from '../http/http.service';
import { CatalogRelation } from '../../models/catalog-relation';
import { ResourceService } from '../resource/resource.service';

import 'rxjs/add/observable/throw';
import { CatalogEntry } from '../../models/catalog-entry';

@Injectable()
export class CatalogService extends HttpService<Catalog> {

  private catalog: Catalog = null;

  private catalogChangedSource: Subject<Catalog> = new Subject<Catalog>();

  catalogChanged$ = this.catalogChangedSource.asObservable();

  constructor(protected resourceService: ResourceService) {    
    super(Catalog, resourceService);
  }

  public onCatalogChanged(catalog: Catalog): void {
    if (!this.catalog || (this.catalog && catalog && this.catalog.id != catalog.id)) {
      this.catalog = catalog;
      this.catalogChangedSource.next(this.catalog);
    }
  }

  public list(catalog: Catalog): Observable<PagedArray<StorageData>>;
  public list(catalog: any): Observable<PagedArray<Catalog>>;
  public list(catalog: Catalog | any): Observable<PagedArray<StorageData>> {
    if (catalog instanceof Catalog) {
      let relation = (catalog._links as CatalogRelation).data;

      return this.relation<StorageData>(StorageData, relation).map(data => {
        let items: PagedArray<StorageData> = this.map(data);
        this.catalog.count = items.totalCount;

        return items;
      });
    }
    
    return Observable.throw("The type '" + typeof catalog + "' of 'catalog' parameter is not supported.");
  }

  public hasValidate() {
    return this.catalog.hasValidate();
  }

  public validate(entity: Catalog): Observable<Catalog> {
    entity._links.validate = this.catalog._links.validate;

    return super.validate(entity);
  }

  public delete(entity: Catalog): Observable<Catalog> {
    return super.delete(entity).map((resource: Catalog) =>{
      this.catalog.count -= 1;

      return resource;
    });
  }

  public hasCreateCatalog(): boolean {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (!relations) {
      return false;
    }

    return relations.createCatalog && !relations.createCatalog.isEmpty();
  }

  public createCatalog(catalog: Catalog): Observable<Catalog> {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (relations) {
      return this.relation<Catalog>(Catalog, relations.createCatalog, catalog).map((resource: Catalog) => {
        this.catalog.count += 1;

        return resource;
      });
    }

    return Observable.throw("No resource found to create catalog");
  }

  public hasCreateFile(): boolean {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (!relations) {
      return false;
    }

    return relations.createFile && !relations.createFile.isEmpty();
  }

  public createFile(entry: CatalogEntry): Observable<CatalogEntry> {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (relations) {
      return this.resourceService.request<CatalogEntry>(CatalogEntry, relations.createFile, entry.toFormData()).map((resource: CatalogEntry) => {
        return resource;
      });
    }

    return Observable.throw("No resource found to create file");
  }
}
