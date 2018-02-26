import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { RightPanelService } from './right-panel.service';

@Component({
  selector: 'app-right-panel',
  templateUrl: './right-panel.component.html',
  styleUrls: ['./right-panel.component.css']
})
export class RightPanelComponent implements OnInit, OnDestroy {

  private visibilityChangedSubscription: Subscription = null;
  public isCollapsed: boolean = false;

  constructor(private rightPanelService:RightPanelService) { }

  ngOnInit() {
    this.isCollapsed = true;

    this.visibilityChangedSubscription = this.rightPanelService.visibilityChanged$.subscribe(isVisible => {
      this.isCollapsed = !isVisible;
    });
  }

  ngOnDestroy(): void {
    if (this.visibilityChangedSubscription) {
      this.visibilityChangedSubscription.unsubscribe();
      this.visibilityChangedSubscription = null;
    }
  }

  public hide() {
    this.rightPanelService.hide();
  }
}
