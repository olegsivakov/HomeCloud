import { Component, Input, OnInit, OnDestroy } from '@angular/core';

import { RightPanelService } from '../../../services/shared/right-panel/right-panel.service';
import { Catalog } from '../../../models/catalog';
import { ISubscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-catalog-details',
  templateUrl: './catalog-details.component.html',
  styleUrls: ['./catalog-details.component.css']
})
export class CatalogDetailsComponent implements OnInit, OnDestroy {

  private rightPanelVisibilityChangedSubscription: ISubscription = null;
  
  @Input()
  public catalog: Catalog = null;

  private get isVisible(): boolean {
    return this.catalog != null;
  }

  constructor(
    private rightPanelService: RightPanelService) { }

  ngOnInit() {
    this.rightPanelVisibilityChangedSubscription = this.rightPanelService.visibilityChanged$.subscribe(isVisible => {
      if (!isVisible) {
        this.catalog = null;
      }
    });
  }

  ngOnDestroy(): void {
    this.catalog = null;
  }
}
