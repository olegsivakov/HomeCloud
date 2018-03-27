import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

import { ResourceArray } from '../../models/http/resource-array';
import { PagedArray } from '../../models/paged-array';
import { StorageRelation } from '../../models/storage-relation';
import { Storage } from '../../models/storage';
import { Catalog } from '../../models/catalog';

import { HttpService } from '../http/http.service';
import { Relation } from '../../models/http/relation';
import { HttpMethod } from '../../models/http/http-method';

const storageUrl: string = "http://localhost:54832/v1/storages";

@Injectable()
export class StorageService extends HttpService<StorageRelation, Storage> {

  private storageSelectedSource: Subject<Storage> = new Subject<Storage>();

  storageSelected$ = this.storageSelectedSource.asObservable();

  constructor(protected httpClient: HttpClient) {
    super(httpClient, storageUrl);
  }

  public catalogs(storage: Storage): Observable<ResourceArray<Catalog>> {
    return this.relations(storage._links.catalogs);
  }

  public select(storage: Storage) {
    this.storageSelectedSource.next(storage);
  }
}
