import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

import { ResourceArray } from '../../models/http/resource-array';
import { StorageRelation } from '../../models/storage-relation';
import { Storage } from '../../models/storage';
import { Catalog } from '../../models/catalog';

import { HttpService } from '../http/http.service';
import { ResourceService } from '../resource/resource.service';

const storageUrl: string = "http://localhost:54832/v1/storages";

@Injectable()
export class StorageService extends HttpService<Storage> {

  private catalogSelectedSource: Subject<Catalog> = new Subject<Catalog>();

  catalogSelected$ = this.catalogSelectedSource.asObservable();

  constructor(protected resourceService: ResourceService) {
    super(Storage, resourceService, storageUrl);
  }

  public catalog(storage: Storage): Observable<Catalog> {
    return this.relation<Catalog>(Catalog, (storage._links as StorageRelation).catalog).map(resource => {
      return resource as Catalog;
    });
  }
}
