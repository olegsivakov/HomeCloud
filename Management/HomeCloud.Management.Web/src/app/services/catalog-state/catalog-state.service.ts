import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

import { Catalog } from '../../models/catalog';

@Injectable()
export class CatalogStateService {

  private catalogChangedSource: Subject<Catalog> = new Subject<Catalog>();

  catalogChanged$: Observable<Catalog> = this.catalogChangedSource.asObservable();

  public catalog: Catalog = null;

  constructor() { }

  public onCatalogChanged(catalog: Catalog) {
    if (!this.catalog || (this.catalog && catalog && (this.catalog !== catalog || this.catalog.id != catalog.id))) {
      this.catalog = catalog;

      this.catalogChangedSource.next(this.catalog);
    }
  }
}
