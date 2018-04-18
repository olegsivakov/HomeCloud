import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { CatalogEntry } from '../../../models/catalog-entry';
import { CatalogEntryState } from '../../../models/catalog-entry-state';

import { Notification } from '../../../models/notifications/notification';
import { NotificationState } from '../../../models/notifications/notification-state';
import { HttpError } from '../../../models/http/http-error';

import { NotificationService } from '../../../services/shared/notification/notification.service';
import { NotificationStateService } from '../../../services/shared/notification-state/notification-state.service';

import { CatalogEntryService } from '../../../services/catalog-entry/catalog-entry.service';
import { RightPanelService } from '../../../services/shared/right-panel/right-panel.service';

@Component({
  selector: 'app-catalog-entry-card',
  templateUrl: './catalog-entry-card.component.html',
  styleUrls: ['./catalog-entry-card.component.css']
})
export class CatalogEntryCardComponent implements OnInit {

  private state: CatalogEntryState = CatalogEntryState.view;
  private errors: Array<string> = new Array<string>();

  private rightPanelVisibilityChangedSubscription: ISubscription = null;
  
  private getSubscription: ISubscription = null;
  private removeSubscription: ISubscription = null;

  private isLoading: boolean = false;
  private isLoaded: boolean = false;

  @Input('entry')
  public entry: CatalogEntry = null;

  @Output('save')
  saveEmitter = new EventEmitter<CatalogEntry>();

  @Output('remove')
  removeEmitter = new EventEmitter<CatalogEntry>();

  constructor(
    private catalogEntryService: CatalogEntryService,
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
    return this.entry.hasGet && this.entry.hasGet() && !this.isLoaded;
  }
  private onLoad(): void {
    if (this.canLoad && !this.isLoading) {
      this.isLoading = true;
      this.load();
    }
  }
  private load() {
    this.getSubscription = this.catalogEntryService.get(this.entry).subscribe(data => {
      this.entry = data;
      this.isLoaded = true;
    }, error => {
      this.isLoading = false;
    });
  }

  public get canSelect(): boolean {
    return false;
  }
  public onSelect(): void {
    if (this.canSelect) {
      //download here
    }
  }

  private get isDetailMode(): boolean {
    return this.state == CatalogEntryState.details;
  }
  private get canDetail(): boolean {
    return this.isLoaded;
  }
  private onDetail(): void {
    if (this.canDetail) {
      this.state = CatalogEntryState.details;
      this.rightPanelService.show();
    }
  }  

  private get isRemoveMode(): boolean {
    return this.state == CatalogEntryState.remove;
  }
  private get canRemove(): boolean {
    return this.entry.hasDelete && this.entry.hasDelete();
  }
  private onRemove(): void {
    if (this.canRemove) {
      this.cancel(true);
      this.state = CatalogEntryState.remove;
    }
  }
  private remove(entry: CatalogEntry): void {
    if (this.canRemove) {
      let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to remove file '" + entry.name + "'.");
      let state: NotificationState = this.notificationStateService.addNotification(notification);

      this.removeSubscription = this.catalogEntryService.delete(entry).subscribe(catalog => {
        this.removeEmitter.emit(this.entry);

        state.setSucceded("Operation complete", "File has been removed successfully.").setExpired();
      }, (error: HttpError) => {
        if (error.statusCode == 404) {
          state.setSucceded("Operation complete", "File has been removed successfully.").setExpired();
        }
        else if(error.statusCode == 500) {
          state.setFailed("Operation failure", "An error occured while removing catalog.").setExpired();
        }
        else {
          state.setFailed("Operation failure", error.messages).setExpired();
        }
      });
    }
  }

  private cancel(forceDetailsCancel?: boolean): void {
    if (forceDetailsCancel && this.state == CatalogEntryState.details) {
      this.rightPanelService.hide();
    }
    else {
      this.state = CatalogEntryState.view;
      this.errors.splice(0, this.errors.length);
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

    if (this.removeSubscription) {
      this.removeSubscription.unsubscribe();
      this.removeSubscription = null;
    }

    this.entry = null;
  }
}
