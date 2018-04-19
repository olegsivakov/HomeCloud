import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ISubscription } from 'rxjs/Subscription';

import { CatalogRelation } from '../../models/catalog-relation';
import { ResourceArray } from '../../models/http/resource-array';
import { Catalog } from '../../models/catalog';
import { CatalogEntry } from '../../models/catalog-entry';

import { HttpService } from '../http/http.service';
import { ResourceService } from '../resource/resource.service';
import { CatalogStateService } from '../catalog-state/catalog-state.service';

import 'rxjs/add/observable/throw';

@Injectable()
export class CatalogEntryService extends HttpService<CatalogEntry> {

  private catalog: Catalog = null;
  private subscription: ISubscription = null;

  constructor(
    protected resourceService: ResourceService,
    private catalogStateService: CatalogStateService) {
    super(CatalogEntry, resourceService);

    this.subscription = this.catalogStateService.catalogChanged$.subscribe(catalog => {
      if (this.catalog && catalog && this.catalog.id != catalog.id) {
        this.resources = new ResourceArray<CatalogEntry>();
      }

      this.catalog = catalog;
    });
  }

  public delete(entity: CatalogEntry): Observable<CatalogEntry> {
    return super.delete(entity).map(resource =>{
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

    return relations.createFile && !relations.createFile.isEmpty();
  }

  public create(entity: CatalogEntry): Observable<CatalogEntry> {
    let relations: CatalogRelation = this.catalog ? this.catalog.getRelations<CatalogRelation>() : null;
    if (relations) {
      return this.resourceService.request<CatalogEntry>(CatalogEntry, relations.createFile, entity.toFormData()).map((resource: CatalogEntry) => {
        this.catalog.count += 1;
        this.catalogStateService.onCatalogChanged(this.catalog);

        return resource;
      });
    }

    return Observable.throw("No resource found to create file");
  }
}
