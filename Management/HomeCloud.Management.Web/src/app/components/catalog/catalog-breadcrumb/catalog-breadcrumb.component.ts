import { Component, OnInit, OnDestroy, EventEmitter } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../../models/catalog';
import { Breadcrumb } from '../../../models/breadcrumbs/breadcrumb';
import { CatalogBreadcrumb } from '../../../models/breadcrumbs/catalog-breadcrumb';

import { CatalogStateService } from '../../../services/catalog-state/catalog-state.service';

@Component({
  selector: 'app-catalog-breadcrumb',
  templateUrl: './catalog-breadcrumb.component.html',
  styleUrls: ['./catalog-breadcrumb.component.css']
})
export class CatalogBreadcrumbComponent implements OnInit, OnDestroy {

  private breadcrumbs: Array<Breadcrumb> = new Array<Breadcrumb>();
  private subscription: ISubscription = null;

  constructor(private catalogStateService: CatalogStateService) {
    this.subscription = this.catalogStateService.catalogChanged$.subscribe(catalog => {
      this.handleCatalog(catalog);
    });
  }

  private open(breadcrumb: CatalogBreadcrumb) {
    this.catalogStateService.onCatalogChanged(breadcrumb.catalog);
  }

  private handleCatalog(catalog: Catalog) {
    let breadcrumb: Breadcrumb = this.breadcrumbs.find(item => item.id == catalog.id);

    if (breadcrumb) {
      let index: number = this.breadcrumbs.indexOf(breadcrumb);
      if (index >= 0) {
        this.breadcrumbs.splice(index + 1);
      }
    }
    else {
      breadcrumb = new CatalogBreadcrumb(catalog);
      this.breadcrumbs.push(breadcrumb);
    }

    breadcrumb.isLast = true;

    if (this.breadcrumbs.length > 1) {
      this.breadcrumbs[this.breadcrumbs.length - 2].isLast = false;
    }
  }

  ngOnInit() {    
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
      this.subscription = null;
    }
  }
}
