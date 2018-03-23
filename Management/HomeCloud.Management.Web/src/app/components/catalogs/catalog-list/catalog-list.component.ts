import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { PagedArray } from '../../../models/paged-array';
import { StorageData } from '../../../models/storage-data';
import { Catalog } from '../../../models/catalog';
import { CatalogState } from '../../../models/catalog-state';
import { CatalogStateChanged } from '../../../models/catalog-state-changed';

import { Notification } from '../../../models/notifications/notification';
import { NotificationState } from '../../../models/notifications/notification-state';

import { CatalogService } from '../../../services/catalog/catalog.service';

import { NotificationService } from '../../../services/shared/notification/notification.service';
import { NotificationStateService } from '../../../services/shared/notification-state/notification-state.service';
import { ProgressService } from '../../../services/shared/progress/progress.service';

@Component({
  selector: 'app-catalog-list',
  templateUrl: './catalog-list.component.html',
  styleUrls: ['./catalog-list.component.css']
})
export class CatalogListComponent implements OnInit, OnDestroy {

  private catalog: Catalog = null;

  private data: PagedArray<StorageData> = new PagedArray<StorageData>();

  private loadSubscription: ISubscription = null;
  private updateSubscription: ISubscription = null;
  private removeSubscription: ISubscription = null;

  constructor(
    private notificationService: NotificationService,
    private notificationStateService: NotificationStateService,
    private progressService: ProgressService,
    private catalogService: CatalogService) {
  }

  ngOnInit() {
    this.catalog = new Catalog();
    this.catalog.ID = "0";
    this.catalog.Name = "Root";

    this.open(this.catalog);
  }

  private open(catalog: Catalog) {
    this.progressService.show();

    this.loadSubscription = this.catalogService.list(20).subscribe(data => {
      this.catalogService.onStateChanged(new CatalogStateChanged(catalog, CatalogState.open));

      this.data = data;

      this.progressService.hide();
    }, error => {
      this.progressService.hide();
    });
  }

  private get canCreate(): boolean {
    return this.catalogService.hasCreate();
  }

  private create() {
    if (this.canCreate) {
      this.catalogService.onStateChanged(new CatalogStateChanged(new Catalog(), CatalogState.edit));
    }
  }
  private edit(catalog: Catalog) {
    this.catalogService.onStateChanged(new CatalogStateChanged(catalog, CatalogState.edit));
  }

  private remove(catalog: Catalog) {
    this.catalogService.onStateChanged(new CatalogStateChanged(catalog, CatalogState.remove));
  }

  private detail(catalog: Catalog) { 
    this.catalogService.onStateChanged(new CatalogStateChanged(catalog, CatalogState.detail));
  }

  private cancel(catalog: Catalog) {
    this.catalogService.onStateChanged(new CatalogStateChanged(catalog, CatalogState.view));
  }

  private save(catalog: Catalog) {
    let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to save catalog '" + catalog.Name + "'.");
    let state: NotificationState = this.notificationStateService.addNotification(notification);    

    this.updateSubscription = this.catalogService.update(catalog).subscribe(catalog => {
      let item: StorageData = this.data.find(item => item.IsCatalog && item.ID == catalog.ID);

      let index: number = this.data.indexOf(item);
      if (index >= 0) {
        this.data.splice(index, 1, catalog);
      }
      
      state.setSucceded("Operation complete", "Catalog '" + catalog.Name + "' has been saved successfully.").setExpired();
    }, error => {
      state.setFailed("Operation failure", "An error occured while saving catalog.").setExpired();
    })
  }

  private delete(catalog: Catalog) {
    let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to remove catalog '" + catalog.Name + "'.");
    let state: NotificationState = this.notificationStateService.addNotification(notification);    

    this.removeSubscription = this.catalogService.delete(catalog).subscribe(catalog => {
      let item: StorageData = this.data.find(item => item.IsCatalog && item.ID == catalog.ID);

      let index: number = this.data.indexOf(item);
      if (index >= 0) {
        this.data.splice(index, 1);
      }

      state.setSucceded("Operation complete", "Catalog '" + catalog.Name + "' has been removed successfully.").setExpired();
    }, error => {
      state.setFailed("Operation failure", "An error occured while removing catalog.").setExpired();
    });
  }

  ngOnDestroy(): void {
    if (this.loadSubscription) {
      this.loadSubscription.unsubscribe();
      this.loadSubscription = null;
    }

    if (this.updateSubscription) {
      this.updateSubscription.unsubscribe();
      this.updateSubscription = null;      
    }

    if (this.removeSubscription) {
      this.removeSubscription.unsubscribe();
      this.removeSubscription = null;      
    }

    this.data.splice(0);
  }
}
