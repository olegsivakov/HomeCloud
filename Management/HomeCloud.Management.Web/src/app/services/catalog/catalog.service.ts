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
      return this.map(data);
    });
    }
    
    return Observable.throw("The type '" + typeof catalog + "' of 'catalog' parameter is not supported.");
  }
}
