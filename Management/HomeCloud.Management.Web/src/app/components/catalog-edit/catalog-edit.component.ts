import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../models/catalog';
import { CatalogService } from '../../services/catalog/catalog.service';

@Component({
  selector: 'app-catalog-edit',
  templateUrl: './catalog-edit.component.html',
  styleUrls: ['./catalog-edit.component.css']
})
export class CatalogEditComponent implements OnInit, OnDestroy {

  private updateRequestedSubscription: ISubscription = null;

  public catalog: Catalog = null;
  
  constructor(private catalogService: CatalogService) { }

  ngOnInit() {
    this.updateRequestedSubscription = this.catalogService.updateRequested$.subscribe(catalog => {
      this.catalog = catalog;
    });
  }

  ngOnDestroy(): void {
    if (this.updateRequestedSubscription) {
      this.updateRequestedSubscription.unsubscribe();
      this.updateRequestedSubscription = null;
    }

    this.cancel();
  }

  public confirm() {
    this.catalogService.save(this.catalog);

    this.cancel();

  }

  public cancel() {
    this.catalog = null;
  }
}
