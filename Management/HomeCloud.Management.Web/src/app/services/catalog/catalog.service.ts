import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Catalog } from '../../models/catalog';
import { CatalogCommand } from '../../models/commands/catalog-command';
import { StorageData } from '../../models/storage-data';

import { Notification } from '../../models/notifications/notification';
import { NotificationState } from '../../models/notifications/notification-state';
import { NotificationService } from '../shared/notification/notification.service';
import { NotificationStateService } from '../shared/notification-state/notification-state.service';
import { ProgressService } from '../shared/progress/progress.service';

@Injectable()
export class CatalogService {

  private openingSource: Subject<CatalogCommand> = new Subject<CatalogCommand>();
  private openedSource: Subject<Array<StorageData>> = new Subject<Array<StorageData>>();

  private savingSource: Subject<CatalogCommand> = new Subject<CatalogCommand>();
  private savedSource: Subject<Catalog> = new Subject<Catalog>();

  private removingSource: Subject<CatalogCommand> = new Subject<CatalogCommand>();
  private removedSource: Subject<Catalog> = new Subject<Catalog>();

  private expandingSource: Subject<CatalogCommand> = new Subject<CatalogCommand>();
  private expandedSource: Subject<Catalog> = new Subject<Catalog>();

  opening$ = this.openingSource.asObservable();
  opened$ = this.openedSource.asObservable();

  saving$ = this.savingSource.asObservable();
  saved$ = this.savedSource.asObservable();

  removing$ = this.removingSource.asObservable();
  removed$ = this.removedSource.asObservable();

  expanding$ = this.expandingSource.asObservable();
  expanded$ = this.expandedSource.asObservable();
  
  constructor(
    private notificationService: NotificationService,
    private notificationStateService: NotificationStateService,
    private progressService: ProgressService) { }

  public createOpenCommand(catalog: Catalog): void {
    if (catalog == null) {
      catalog = new Catalog();
      catalog.ID = "0";
      catalog.Name = "Root";
    }

    let command: CatalogCommand = new CatalogCommand(catalog);

    this.openingSource.next(command);

  }

  public executeOpenCommand(command: CatalogCommand): void {
    command.execute(catalog => {
      this.progressService.show();

      setTimeout(() => {
        let data: Array<StorageData> = this.Initialize(catalog);
        this.openedSource.next(data);

        this.progressService.hide();
      }, 1500);
    });
  }

  public createExpandCommand(catalog: Catalog): void {
    let command: CatalogCommand = new CatalogCommand(catalog);

    this.expandingSource.next(command);

  }

  public executeExpandCommand(command: CatalogCommand): void {
    command.execute(catalog => {
      this.expandedSource.next(catalog);
    });
  }

  public createSaveCommand(catalog: Catalog): void {
    let command: CatalogCommand = new CatalogCommand(catalog);

    this.savingSource.next(command);
  }

  public executeSaveCommand(command: CatalogCommand): void {
    command.execute(catalog => {

      let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to save  catalog '" + catalog.Name + "'.");
      let state: NotificationState = this.notificationStateService.addNotification(notification);    

      setTimeout(() => {
        state.setSucceded("Operation complete", "Catalog '" + catalog.Name + "' has been saved successfully.").setExpired();

        this.savedSource.next(catalog);
      }, 3000);

      setTimeout(() => {
        state.setFailed("Operation failure", "An error occured while saving catalog.").setExpired();
      }, 5000);      
    });
  }

  public createRemoveCommand(catalog: Catalog): void {
    let command: CatalogCommand = new CatalogCommand(catalog);

    this.removingSource.next(command);
  }

  public executeRemoveCommand(command: CatalogCommand): void {
    command.execute(catalog => {

      let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to remove catalog '" + catalog.Name + "'.");
      let state: NotificationState = this.notificationStateService.addNotification(notification);    

      setTimeout(() => {
        state.setSucceded("Operation complete", "Catalog '" + catalog.Name + "' has been removed successfully.").setExpired();

        this.removedSource.next(catalog);
      }, 3000);

      setTimeout(() => {
        state.setFailed("Operation failure", "An error occured while removing catalog.").setExpired();
      }, 5000);   
    });
  }

  private Initialize(parent: Catalog): Array<StorageData> {
    let data: Array<StorageData> = new Array<StorageData>();

    if (parent.ID == "0") {

      let data1: Catalog = new Catalog();
        data1.ID = "1";
        data1.Name = "Catalog 1";
        data1.CreationDate = new Date();
        data1.Size = "15Mb";
    
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
    
        let data2: Catalog = new Catalog();
        data2.ID = "1";
        data2.Name = "Catalog 2";
        data2.CreationDate = new Date();
        data2.Size = "20Mb";
    
        data.push(data2);
    }
    else if (parent.ID == "1"){
      let data3: Catalog = new Catalog();
      data3.ID = "3";
      data3.Name = "Catalog 3";
      data3.CreationDate = new Date();
      data3.Size = "200Mb";
    
      data.push(data3);
    }
    else {
      let data4: Catalog = new Catalog();
      data4.ID = "4";
      data4.Name = "Catalog 4";
      data4.CreationDate = new Date();
      data4.Size = "256Gb";
    
      data.push(data4);
    }

    return data;
  }
}
