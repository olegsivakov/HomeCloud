import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Catalog } from '../../models/catalog';

import { Notification } from '../../models/notification';
import { NotificationState } from '../../models/notification-state';
import { NotificationService } from '../shared/notification/notification.service';
import { NotificationStateService } from '../shared/notification-state/notification-state.service';

@Injectable()
export class CatalogService {

  private detailsRequestedSource = new Subject<Catalog>();
  private updateRequestedSource = new Subject<Catalog>();
  private removeRequestedSource = new Subject<Catalog>();
  private catalogSavedSource = new Subject<Catalog>();
  private catalogRemovedSource = new Subject<Catalog>();

  detailsRequested$ = this.detailsRequestedSource.asObservable();
  updateRequested$ = this.updateRequestedSource.asObservable();
  removeRequested$ = this.removeRequestedSource.asObservable();
  catalogSaved$ = this.catalogSavedSource.asObservable();
  catalogRemoved$ = this.catalogRemovedSource.asObservable();
  
  constructor(
    private notificationService: NotificationService,
    private notificationStateService: NotificationStateService) { }

  public requestDetails(catalog: Catalog): void {
    this.detailsRequestedSource.next(catalog);
  }

  public requestUpdate(catalog: Catalog): void {
    this.updateRequestedSource.next(catalog);
  }

  public requestRemove(catalog: Catalog): void {
    this.removeRequestedSource.next(catalog);
  }

  public save(catalog: Catalog): void {
    let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to save  catalog '" + catalog.Name + "'.");
    let state: NotificationState = this.notificationStateService.addNotification(notification);    

    setTimeout(() => {
      state.setSucceded("Operation complete", "Catalog '" + catalog.Name + "' has been saved successfully.").setExpired();

      this.catalogSavedSource.next(catalog);
    }, 3000);

    setTimeout(() => {
      state.setFailed("Operation failure", "An error occured while saving catalog.").setExpired();
    }, 5000);
  }

  public remove(catalog: Catalog): void {
    let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to remove catalog '" + catalog.Name + "'.");
    let state: NotificationState = this.notificationStateService.addNotification(notification);    

    setTimeout(() => {
      state.setSucceded("Operation complete", "Catalog '" + catalog.Name + "' has been removed successfully.").setExpired();

      this.catalogRemovedSource.next(catalog);
    }, 3000);

    setTimeout(() => {
      state.setFailed("Operation failure", "An error occured while removing catalog.").setExpired();
    }, 5000);
  }
}
