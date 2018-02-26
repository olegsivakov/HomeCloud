import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Catalog } from '../../models/catalog';

@Injectable()
export class CatalogService {

  private detailsRequestedSource = new Subject<Catalog>();
  private updateRequestedSource = new Subject<Catalog>();
  private removeRequestedSource = new Subject<Catalog>();
  private catalogSavedSource = new Subject<Catalog>();
  private catalogSavedFailedSource = new Subject<Catalog>();
  private catalogRemovedSource = new Subject<Catalog>();
  private catalogRemovedFailedSource = new Subject<Catalog>();

  detailsRequested$ = this.detailsRequestedSource.asObservable();
  updateRequested$ = this.updateRequestedSource.asObservable();
  removeRequested$ = this.removeRequestedSource.asObservable();
  catalogSaved$ = this.catalogSavedSource.asObservable();
  catalogSavedFailed$ = this.catalogSavedFailedSource.asObservable();
  catalogRemoved$ = this.catalogRemovedSource.asObservable();
  catalogRemovedFailed$ = this.catalogRemovedFailedSource.asObservable();
  
  constructor() { }

  public requestDetails(catalog: Catalog): void {
    this.detailsRequestedSource.next(catalog);
  }

  public requestUpdate(catalog: Catalog): void {
    this.updateRequestedSource.next(catalog);
  }

  public requestRemove(catalog: Catalog): void {
    this.removeRequestedSource.next(catalog);
  }

  public save(catalog: Catalog): void {
    this.catalogSavedSource.next(catalog);
  }

  public remove(catalog: Catalog): void {
    this.catalogRemovedSource.next(catalog);
  }
}
