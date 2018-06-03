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
import { environment } from '../../../environments/environment';

@Injectable()
export class StorageService extends HttpService<Storage> {

  private catalogSubscription: ISubscription = null;
  
  constructor(
    protected resourceService: ResourceService,
    private catalogStateService: CatalogStateService) {
    super(Storage, resourceService, environment.dataStorageUrl + "/storages");
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
