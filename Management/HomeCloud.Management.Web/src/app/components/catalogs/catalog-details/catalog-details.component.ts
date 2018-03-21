import { Component, Input, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../../models/catalog';
import { CatalogState } from '../../../models/catalog-state';

import { RightPanelService } from '../../../services/shared/right-panel/right-panel.service';
import { CatalogDataService } from '../../../services/catalog/catalog-data.service';

@Component({
  selector: 'app-catalog-details',
  templateUrl: './catalog-details.component.html',
  styleUrls: ['./catalog-details.component.css']
})
export class CatalogDetailsComponent implements OnInit, OnDestroy {

  private rightPanelVisibilityChangedSubscription: ISubscription = null;
  private stateChangedSubscription: ISubscription = null;

  private catalog: Catalog = null;

  constructor(
    private rightPanelService: RightPanelService,
    private catalogService: CatalogDataService) {
      this.rightPanelVisibilityChangedSubscription = this.rightPanelService.visibilityChanged$.subscribe(isVisible => {
        if (!isVisible) {
          this.catalog = null;
        }        
      });
  
      this.stateChangedSubscription = this.catalogService.stateChanged$.subscribe(args => {
        if (args.state == CatalogState.detail) {
          this.catalog = args.catalog;
          this.rightPanelService.show();
        }
        else if (this.catalog) {          
          this.rightPanelService.hide();
        }
      });
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    if (this.rightPanelVisibilityChangedSubscription) {
      this.rightPanelVisibilityChangedSubscription.unsubscribe();
      this.rightPanelVisibilityChangedSubscription = null;
    }

    if (this.stateChangedSubscription) {
      this.stateChangedSubscription.unsubscribe();
      this.stateChangedSubscription = null;
    }

    this.catalog = null;
  }
}
