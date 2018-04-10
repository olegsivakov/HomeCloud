import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { PagedArray } from '../../../models/paged-array';
import { StorageData } from '../../../models/storage-data';
import { Catalog } from '../../../models/catalog';

import { Notification } from '../../../models/notifications/notification';
import { NotificationState } from '../../../models/notifications/notification-state';

import { CatalogService } from '../../../services/catalog/catalog.service';
import { ProgressService } from '../../../services/shared/progress/progress.service';
import { NotificationService } from '../../../services/shared/notification/notification.service';
import { NotificationStateService } from '../../../services/shared/notification-state/notification-state.service';

@Component({
  selector: 'app-catalog-list',
  templateUrl: './catalog-list.component.html',
  styleUrls: ['./catalog-list.component.css']
})
export class CatalogListComponent implements OnInit, OnDestroy {

  private newCatalog: Catalog = null;
  private data: PagedArray<StorageData> = new PagedArray<StorageData>();

  private catalogChangedSubscription: ISubscription = null;
  private listSubscription: ISubscription = null;
  private createCatalogSubscription: ISubscription = null;

  constructor(
    private progressService: ProgressService,
    private catalogService: CatalogService,
    private notificationService: NotificationService,
    private notificationStateService: NotificationStateService) {
      this.catalogChangedSubscription = this.catalogService.catalogChanged$.subscribe(catalog => {        
        this.data.splice(0);
        this.open(catalog);
      });
    }

  ngOnInit() {
  }

  private open(catalog: Catalog) {
    this.progressService.show();

    this.listSubscription = this.catalogService.list(catalog).subscribe(data => {
      this.data = data;

      this.progressService.hide();
    }, error => {
      this.progressService.hide();
    });
  }

  private get isCreateCatalogMode(): boolean {
    return this.newCatalog != null;
  }  
  private get canCreateCatalog(): boolean {
    return this.catalogService.hasCreateCatalog();
  }
  private onCreateCatalog() {
    if (this.canCreateCatalog) {
      this.newCatalog = new Catalog();
    }
  }
  private createCatalog(catalog: Catalog) {
    if (this.canCreateCatalog) {
      let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to create catalog '" + catalog.name + "'.");
      let state: NotificationState = this.notificationStateService.addNotification(notification);    

      this.createCatalogSubscription = this.catalogService.createCatalog(catalog).subscribe(catalog => {         
        state.setSucceded("Operation complete", "Catalog '" + catalog.name + "' has been created successfully.").setExpired();
      }, error => {
        state.setFailed("Operation failure", "An error occured while creating catalog.").setExpired();
      });
    }
  }

  private remove(catalog: Catalog) {    
    let item: StorageData = this.data.find(item => item.isCatalog && item.id == catalog.id);

    let index: number = this.data.indexOf(item);
    if (index >= 0) {
      this.data.splice(index, 1);
    }
  }

  private save(catalog: Catalog) {
    // let item: StorageData = this.data.find(item => item.isCatalog && item.id == catalog.id);

    // let index: number = this.data.indexOf(item);
    // if (index >= 0) {
    //   this.data[index] = catalog;
    // }
  }

  private cancel() {
    if (this.isCreateCatalogMode) {
      this.newCatalog = null;
    }
  }

  ngOnDestroy(): void {

    if (this.catalogChangedSubscription) {
      this.catalogChangedSubscription.unsubscribe();
      this.catalogChangedSubscription = null;
    }

    if (this.listSubscription) {
      this.listSubscription.unsubscribe();
      this.listSubscription = null;
    }

    if (this.createCatalogSubscription) {
      this.createCatalogSubscription.unsubscribe();
      this.createCatalogSubscription = null;
    }

    this.data.splice(0);
    this.data = null;
  }
}
