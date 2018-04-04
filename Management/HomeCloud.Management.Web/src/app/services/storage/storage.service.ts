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

  private storageSelectedSource: Subject<Storage> = new Subject<Storage>();

  storageSelected$ = this.storageSelectedSource.asObservable();

  constructor(protected resourceService: ResourceService) {
    super(Storage, resourceService, storageUrl);
  }

  public catalogs(storage: Storage): Observable<ResourceArray<Catalog>> {
    return this.relation<Catalog>(Catalog, (storage._links as StorageRelation).catalogs).map(resource => {
      return resource as ResourceArray<Catalog>;
    });
  }

  public select(storage: Storage) {
    this.storageSelectedSource.next(storage);
  }
}
