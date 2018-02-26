import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../models/catalog';
import { CatalogService } from '../catalog/catalog.service';

@Component({
  selector: 'app-catalog-edit',
  templateUrl: './catalog-edit.component.html',
  styleUrls: ['./catalog-edit.component.css']
})
export class CatalogEditComponent implements OnInit, OnDestroy {

  private updateRequestedSubscription: ISubscription = null;
  private catalogSavedSubscription: ISubscription = null;
  private catalogSaveFailedSubscription: ISubscription = null;

  public catalog: Catalog = null;
  
  constructor(private catalogService: CatalogService) { }

  ngOnInit() {
    this.updateRequestedSubscription = this.catalogService.updateRequested$.subscribe(catalog => {
      this.catalog = catalog;
    });

    this.catalogSavedSubscription = this.catalogService.catalogSaved$.subscribe(catalog => {
      this.catalog = null;
    });

    this.catalogSaveFailedSubscription = this.catalogService.catalogSavedFailed$.subscribe(catalog => {
      this.catalog = catalog;
    });
  }

  ngOnDestroy(): void {
    if (this.updateRequestedSubscription) {
      this.updateRequestedSubscription.unsubscribe();
      this.updateRequestedSubscription = null;
    }

    if (this.catalogSavedSubscription) {
      this.catalogSavedSubscription.unsubscribe();
      this.catalogSavedSubscription = null;
    }

    if (this.catalogSaveFailedSubscription) {
      this.catalogSaveFailedSubscription.unsubscribe();
      this.catalogSaveFailedSubscription = null;
    }

    this.catalog = null;
  }

  public confirm() {
    this.catalogService.save(this.catalog);

  }

  public cancel() {
    this.catalog = null;
  }
}
