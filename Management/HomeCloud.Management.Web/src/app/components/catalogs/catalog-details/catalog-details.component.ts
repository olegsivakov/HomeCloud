import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../../models/catalog';
import { CatalogCommand } from '../../../models/commands/catalog-command';
import { CatalogService } from '../../../services/catalog/catalog.service';

import { RightPanelService } from '../../../services/shared/right-panel/right-panel.service';

@Component({
  selector: 'app-catalog-details',
  templateUrl: './catalog-details.component.html',
  styleUrls: ['./catalog-details.component.css']
})
export class CatalogDetailsComponent implements OnInit, OnDestroy {

  private expandingSubscription: ISubscription = null;
  private expandedSubscription: ISubscription = null;
  private rightPanelVisibilityChangedSubscription: ISubscription = null;

  private command: CatalogCommand = null;
  private catalog: Catalog = null;

  constructor(private catalogService: CatalogService, private rightPanelService: RightPanelService) { }

  private get isVisible(): boolean {
    return this.catalog != null;
  }

  ngOnInit() {
    this.expandedSubscription = this.catalogService.expanded$.subscribe(catalog => {
      this.catalog = catalog;
    });

    this.expandingSubscription = this.catalogService.expanding$.subscribe(command => {
      this.command = command;
      this.rightPanelService.show();
    });

    this.rightPanelVisibilityChangedSubscription = this.rightPanelService.visibilityChanged$.subscribe(isVisible => {
      if (!isVisible) {
        this.command = null;
        this.catalog = null;
      }
      else {
        this.catalogService.executeExpandCommand(this.command);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.expandingSubscription) {
      this.expandingSubscription.unsubscribe();
      this.expandingSubscription = null;
    }

    if (this.expandedSubscription) {
      this.expandedSubscription.unsubscribe();
      this.expandedSubscription = null;
    }

    if (this.rightPanelVisibilityChangedSubscription) {
      this.rightPanelVisibilityChangedSubscription.unsubscribe();
      this.rightPanelVisibilityChangedSubscription = null;
    }

    this.command = null;
    this.catalog = null;
  }
}
