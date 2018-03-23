import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

import { RelationArray } from '../../models/http/relation-array';
import { PagedArray } from '../../models/paged-array';
import { Catalog } from '../../models/catalog';
import { StorageData } from '../../models/storage-data';
import { CatalogStateChanged } from '../../models/catalog-state-changed';

import { HttpService } from '../http/http.service';

const url: string = "http://localhost:54832/v1/catalogs/c97ae41c-53d8-480c-9c16-bf03bb93f3ae";

@Injectable()
export class CatalogService extends HttpService<RelationArray, Catalog> {

  private stateChangedSource: Subject<CatalogStateChanged> = new Subject<CatalogStateChanged>();

  stateChanged$ = this.stateChangedSource.asObservable();

  constructor(protected httpClient: HttpClient) {    
    super(httpClient, url);
  }

  public onStateChanged(args: CatalogStateChanged): void {
    this.stateChangedSource.next(args);
  }
}
