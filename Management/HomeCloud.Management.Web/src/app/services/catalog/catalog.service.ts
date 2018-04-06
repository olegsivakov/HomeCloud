import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

import { PagedArray } from '../../models/paged-array';
import { Catalog } from '../../models/catalog';
import { StorageData } from '../../models/storage-data';
import { CatalogStateChanged } from '../../models/catalog-state-changed';

import { HttpService } from '../http/http.service';
import { CatalogRelation } from '../../models/catalog-relation';
import { ResourceService } from '../resource/resource.service';

import 'rxjs/add/observable/throw';

@Injectable()
export class CatalogService extends HttpService<Catalog> {

  public catalog: Catalog = null;

  private stateChangedSource: Subject<CatalogStateChanged> = new Subject<CatalogStateChanged>();

  stateChanged$ = this.stateChangedSource.asObservable();

  constructor(protected resourceService: ResourceService) {    
    super(Catalog, resourceService);
  }

  public onStateChanged(args: CatalogStateChanged): void {
    this.stateChangedSource.next(args);
  }

  public list(catalog: Catalog): Observable<PagedArray<StorageData>>;
  public list(catalog: any): Observable<PagedArray<Catalog>>;
  public list(catalog: Catalog | any): Observable<PagedArray<StorageData>> {
    if (catalog instanceof Catalog) {
      let relation = (catalog._links as CatalogRelation).data;

    return this.relation<StorageData>(StorageData, relation).map(data => {
      this.catalog = catalog;
      return this.map(data);
    });
    }
    
    return Observable.throw("The type '" + typeof catalog + "' of 'catalog' parameter is not supported.");
  }

  public hasCreateCatalog(): boolean {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (!relations) {
      return false;
    }

    return relations.createCatalog && !relations.createCatalog.isEmpty();
  }

  public createCatalog(catalog: Catalog): Catalog {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (relations) {
      this.relation<Catalog>(Catalog, this.catalog.getRelations)
    }
  }

  public hasCreateFile(): boolean {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (!relations) {
      return false;
    }

    return relations.createFile && !relations.createFile.isEmpty();
  }

  public createFile(catalog: Catalog): any {
    return null;
  }
}
