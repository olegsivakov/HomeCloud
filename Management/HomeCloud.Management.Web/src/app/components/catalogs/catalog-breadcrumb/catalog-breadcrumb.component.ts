import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../../models/catalog';
import { CatalogService } from '../../../services/catalog/catalog.service';
import { Breadcrumb } from '../../../models/breadcrumbs/breadcrumb';
import { CatalogBreadcrumb } from '../../../models/breadcrumbs/catalog-breadcrumb';

@Component({
  selector: 'app-catalog-breadcrumb',
  templateUrl: './catalog-breadcrumb.component.html',
  styleUrls: ['./catalog-breadcrumb.component.css']
})
export class CatalogBreadcrumbComponent implements OnInit, OnDestroy {

  private catalogOpeningSubscription: ISubscription = null;
  private breadcrumbs: Array<Breadcrumb> = new Array<Breadcrumb>();

  constructor(
    private catalogService: CatalogService
  ) {
    this.catalogOpeningSubscription = this.catalogService.opening$.subscribe(command => {
      this.handleCatalog(command.catalog);
    });
   }

  private open(breadcrumb: CatalogBreadcrumb) {
    this.catalogService.createOpenCommand(breadcrumb.catalog);
  }

  private handleCatalog(catalog: Catalog) {
    let breadcrumb: Breadcrumb = this.breadcrumbs.find(item => item.id == catalog.ID);

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
    if (this.catalogOpeningSubscription) {
      this.catalogOpeningSubscription.unsubscribe();
      this.catalogOpeningSubscription = null;
    }
  }
}
