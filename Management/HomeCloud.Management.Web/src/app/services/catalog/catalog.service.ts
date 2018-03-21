import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Catalog } from '../../models/catalog';
import { StorageData } from '../../models/storage-data';

import { Notification } from '../../models/notifications/notification';
import { NotificationState } from '../../models/notifications/notification-state';
import { NotificationService } from '../shared/notification/notification.service';
import { NotificationStateService } from '../shared/notification-state/notification-state.service';
import { ProgressService } from '../shared/progress/progress.service';
import { CatalogDataService } from './catalog-data.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class CatalogService {

  private loadedSource: Subject<Array<StorageData>> = new Subject<Array<StorageData>>();
  private selectedSource: Subject<Catalog> = new Subject<Catalog>();
  private savedSource: Subject<Catalog> = new Subject<Catalog>();
  private removedSource: Subject<Catalog> = new Subject<Catalog>();
  private expandedSource: Subject<Catalog> = new Subject<Catalog>();

  loaded$ = this.loadedSource.asObservable();
  selected$ = this.selectedSource.asObservable();
  saved$ = this.savedSource.asObservable();
  removed$ = this.removedSource.asObservable();
  expanded$ = this.expandedSource.asObservable();
  
  constructor(
    private notificationService: NotificationService,
    private notificationStateService: NotificationStateService,
    private progressService: ProgressService,
    private catalogDataService: CatalogDataService) { }

  public load(catalog?: Catalog): void {
    if (catalog == null) {
      catalog = new Catalog();
      catalog.ID = "0";
      catalog.Name = "Root";
    }
  }

  public create(catalog: Catalog): Observable<Catalog> {
    return this.catalogDataService.create(catalog);
  }

  public save(catalog: Catalog): Observable<Catalog> {
    return this.catalogDataService.update(catalog);
  }

  public remove(catalog: Catalog): Observable<Catalog> {
    return this.catalogDataService.delete(catalog);
  }

  // public createOpenCommand(catalog: Catalog): void {
  //   if (catalog == null) {
  //     catalog = new Catalog();
  //     catalog.ID = "0";
  //     catalog.Name = "Root";

  //     this.catalogDataService.
  //   }

  //   let command: CatalogCommand = new CatalogCommand(catalog);

  //   this.openingSource.next(command);

  // }

  // public executeOpenCommand(command: CatalogCommand): void {
  //   command.execute(catalog => {
  //     this.progressService.show();

  //     setTimeout(() => {
  //       let data: Array<StorageData> = this.Initialize(catalog);
  //       this.openedSource.next(data);

  //       this.progressService.hide();
  //     }, 1500);
  //   });
  // }

  // public createExpandCommand(catalog: Catalog): void {
  //   let command: CatalogCommand = new CatalogCommand(catalog);

  //   this.expandingSource.next(command);

  // }

  // public executeExpandCommand(command: CatalogCommand): void {
  //   command.execute(catalog => {
  //     this.expandedSource.next(catalog);
  //   });
  // }

  // public createSaveCommand(catalog: Catalog): void {
  //   let command: CatalogCommand = new CatalogCommand(catalog);

  //   this.savingSource.next(command);
  // }

  // public executeSaveCommand(command: CatalogCommand): void {
  //   command.execute(catalog => {

  //     let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to save  catalog '" + catalog.Name + "'.");
  //     let state: NotificationState = this.notificationStateService.addNotification(notification);    

  //     setTimeout(() => {
  //       state.setSucceded("Operation complete", "Catalog '" + catalog.Name + "' has been saved successfully.").setExpired();

  //       this.savedSource.next(catalog);
  //     }, 3000);

  //     setTimeout(() => {
  //       state.setFailed("Operation failure", "An error occured while saving catalog.").setExpired();
  //     }, 5000);      
  //   });
  // }

  // public createRemoveCommand(catalog: Catalog): void {
  //   let command: CatalogCommand = new CatalogCommand(catalog);

  //   this.removingSource.next(command);
  // }

  // public executeRemoveCommand(command: CatalogCommand): void {
  //   command.execute(catalog => {

  //     let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to remove catalog '" + catalog.Name + "'.");
  //     let state: NotificationState = this.notificationStateService.addNotification(notification);    

  //     setTimeout(() => {
  //       state.setSucceded("Operation complete", "Catalog '" + catalog.Name + "' has been removed successfully.").setExpired();

  //       this.removedSource.next(catalog);
  //     }, 3000);

  //     setTimeout(() => {
  //       state.setFailed("Operation failure", "An error occured while removing catalog.").setExpired();
  //     }, 5000);   
  //   });
  // }

  
}
