import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../models/catalog';
import { CatalogService } from '../../services/catalog/catalog.service';

@Component({
  selector: 'app-catalog-remove',
  templateUrl: './catalog-remove.component.html',
  styleUrls: ['./catalog-remove.component.css']
})
export class CatalogRemoveComponent implements OnInit, OnDestroy {

  private removeRequestedSubscription: ISubscription = null;

  public catalog: Catalog = null;

  constructor(
    private catalogService: CatalogService) { }

  ngOnInit() {
    this.removeRequestedSubscription = this.catalogService.removeRequested$.subscribe(catalog => {
      this.catalog = catalog;
    });
  }

  ngOnDestroy(): void {
    if (this.removeRequestedSubscription) {
      this.removeRequestedSubscription.unsubscribe();
      this.removeRequestedSubscription = null;
    }

    this.cancel();
  }

  public confirm() {
    this.catalogService.remove(this.catalog);

    this.cancel();
  }

  public cancel() {
    this.catalog = null;
  }
}
