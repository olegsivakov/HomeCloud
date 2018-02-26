import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../models/catalog';
import { CatalogService } from '../catalog/catalog.service';
import { RightPanelService } from '../shared/right-panel/right-panel.service';

@Component({
  selector: 'app-catalog-details',
  templateUrl: './catalog-details.component.html',
  styleUrls: ['./catalog-details.component.css']
})
export class CatalogDetailsComponent implements OnInit, OnDestroy {

  private detailsRequestedSubscription: ISubscription = null;
  private rightPanelVisibilityChangedSubscription: ISubscription = null;
  
  public catalog: Catalog = null;

  constructor(private catalogService: CatalogService, private rightPanelService: RightPanelService) { }

  ngOnInit() {
    this.detailsRequestedSubscription = this.catalogService.detailsRequested$.subscribe(catalog => {
      this.catalog = catalog;
      this.rightPanelService.show();
    });

    this.rightPanelVisibilityChangedSubscription = this.rightPanelService.visibilityChanged$.subscribe(isVisible => {
      if (!isVisible) {
        this.catalog = null;
      }
    });
  }

  ngOnDestroy(): void {
    if (this.detailsRequestedSubscription) {
      this.detailsRequestedSubscription.unsubscribe();
      this.detailsRequestedSubscription = null;
    }

    if (this.rightPanelVisibilityChangedSubscription) {
      this.rightPanelVisibilityChangedSubscription.unsubscribe();
      this.rightPanelVisibilityChangedSubscription = null;
    }

    this.catalog = null;
  }
}
