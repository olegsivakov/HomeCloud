import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../../models/catalog';
import { CatalogRelation } from '../../../models/catalog-relation';
import { CatalogState } from '../../../models/catalog-state';

import { Notification } from '../../../models/notifications/notification';
import { NotificationState } from '../../../models/notifications/notification-state';

import { NotificationService } from '../../../services/shared/notification/notification.service';
import { NotificationStateService } from '../../../services/shared/notification-state/notification-state.service';

import { CatalogService } from '../../../services/catalog/catalog.service';
import { RightPanelService } from '../../../services/shared/right-panel/right-panel.service';

@Component({
  selector: 'app-catalog-card',
  templateUrl: './catalog-card.component.html',
  styleUrls: ['./catalog-card.component.css']
})
export class CatalogCardComponent implements OnInit, OnDestroy {

  private state: CatalogState = CatalogState.view;

  private getSubscription: ISubscription = null;
  private rightPanelVisibilityChangedSubscription: ISubscription = null;

  private updateSubscription: ISubscription = null;
  private removeSubscription: ISubscription = null;

  private isLoading: boolean = false;

  @Input('catalog')
  public catalog: Catalog = null;

  @Output('save')
  saveEmitter = new EventEmitter<Catalog>();

  @Output('remove')
  removeEmitter = new EventEmitter<Catalog>();

  constructor(
    private catalogService: CatalogService,
    private rightPanelService: RightPanelService,  
    private notificationService: NotificationService,
    private notificationStateService: NotificationStateService) {
      this.rightPanelVisibilityChangedSubscription = this.rightPanelService.visibilityChanged$.subscribe(isVisible => {
        if (!isVisible) {
          this.cancel();
        }
      });
    }

  private get canLoad(): boolean {
    return this.catalog.hasGet && this.catalog.hasGet() && !(this.catalog._links instanceof CatalogRelation);
  }
  private onLoad(): void {
    if (this.canLoad && !this.isLoading) {
      this.isLoading = true;
      this.load();
    }
  }
  private load() {
    this.getSubscription = this.catalogService.get(this.catalog).subscribe(data => {
      this.catalog = data;
    });
  }

  public get canSelect(): boolean {
    return this.catalog.hasData && this.catalog.hasData();
  }
  public onSelect(): void {
    if (this.canSelect) {
      this.catalogService.onCatalogChanged(this.catalog);
    }
  }

  private get isDetailMode(): boolean {
    return this.state == CatalogState.details;
  }
  private get canDetail(): boolean {
    return this.catalog.hasSelf && this.catalog.hasSelf();
  }
  private onDetail(): void {
    if (this.canDetail) {
      this.state = CatalogState.details;
      this.rightPanelService.show();
    }
  }

  private get isEditMode(): boolean {
    return this.state == CatalogState.edit;
  }
  private get canEdit(): boolean {
    return this.catalog.hasUpdate && this.catalog.hasUpdate();
  }
  private onEdit(): void {
    if (this.canEdit) {
      this.cancel(true);
      this.state = CatalogState.edit;
    }
  }
  private save(catalog: Catalog): void {
    if (this.canEdit) {
      let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to save catalog '" + catalog.name + "'.");
      let state: NotificationState = this.notificationStateService.addNotification(notification);    

      this.updateSubscription = this.catalogService.update(catalog).subscribe(catalog => {
        this.catalog = catalog;
        this.load();
        this.saveEmitter.emit(this.catalog);
        state.setSucceded("Operation complete", "Catalog '" + catalog.name + "' has been saved successfully.").setExpired();
      }, error => {
        state.setFailed("Operation failure", "An error occured while saving catalog.").setExpired();
      });
    }
  }

  private get isRemoveMode(): boolean {
    return this.state == CatalogState.remove;
  }
  private get canRemove(): boolean {
    return this.catalog.hasDelete && this.catalog.hasDelete();
  }
  private onRemove(): void {
    if (this.canRemove) {
      this.cancel(true);
      this.state = CatalogState.remove;
    }
  }
  private remove(catalog: Catalog): void {
    if (this.canRemove) {
      let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to remove catalog '" + catalog.name + "'.");
      let state: NotificationState = this.notificationStateService.addNotification(notification);

      this.removeSubscription = this.catalogService.delete(catalog).subscribe(catalog => {
        this.removeEmitter.emit(this.catalog);

        state.setSucceded("Operation complete", "Catalog has been removed successfully.").setExpired();
      }, error => {
        state.setFailed("Operation failure", "An error occured while removing catalog.").setExpired();
      });
    }
  }

  private cancel(forceDetailsCancel?: boolean): void {
    if (forceDetailsCancel && this.state == CatalogState.details) {
      this.rightPanelService.hide();
    }
    else {
      this.state = CatalogState.view;
    }
  }  

  ngOnInit() {
  }

  ngOnDestroy(): void {
    if (this.rightPanelVisibilityChangedSubscription) {
      this.rightPanelVisibilityChangedSubscription.unsubscribe();
      this.rightPanelVisibilityChangedSubscription = null;
    }

    if (this.getSubscription) {
      this.getSubscription.unsubscribe();
      this.getSubscription = null;
    }

    if (this.updateSubscription) {
      this.updateSubscription.unsubscribe();
      this.updateSubscription = null;
    }

    if (this.removeSubscription) {
      this.removeSubscription.unsubscribe();
      this.removeSubscription = null;
    }

    this.catalog = null;
  }
}
