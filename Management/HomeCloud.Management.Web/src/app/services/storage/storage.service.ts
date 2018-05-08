import { Injectable } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';

import { ResourceArray } from '../../models/http/resource-array';
import { StorageRelation } from '../../models/storage-relation';
import { Storage } from '../../models/storage';
import { Catalog } from '../../models/catalog';

import { HttpService } from '../http/http.service';
import { ResourceService } from '../resource/resource.service';
import { CatalogStateService } from '../catalog-state/catalog-state.service';

const storageUrl: string = "http://localhost:8081/v1/storages";

@Injectable()
export class StorageService extends HttpService<Storage> {

  private catalogSubscription: ISubscription = null;
  
  constructor(
    protected resourceService: ResourceService,
    private catalogStateService: CatalogStateService) {
    super(Storage, resourceService, storageUrl);
  }

  public catalog(storage: Storage): Observable<Catalog> {
    return this.relation<Catalog>(Catalog, (storage._links as StorageRelation).catalog).map(resource => {
      return resource as Catalog;
    });
  }

  public selectCatalog(storage: Storage): void {
    if (storage.hasCatalog()) {
      this.catalogSubscription = this.catalog(storage).subscribe(catalog => {
        this.catalogStateService.onCatalogChanged(catalog);
      });
    }
  }
}
