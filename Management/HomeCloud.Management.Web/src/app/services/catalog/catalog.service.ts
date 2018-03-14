import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Catalog } from '../../models/catalog';

import { Notification } from '../../models/notification';
import { NotificationState } from '../../models/notification-state';
import { NotificationService } from '../shared/notification/notification.service';
import { NotificationStateService } from '../shared/notification-state/notification-state.service';
import { CatalogCommand } from '../../models/commands/catalog-command';

@Injectable()
export class CatalogService {

  private openingSource: Subject<CatalogCommand> = new Subject<CatalogCommand>();
  private openedSource: Subject<Catalog> = new Subject<Catalog>();

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
    private notificationStateService: NotificationStateService) { }

  public createOpenCommand(catalog: Catalog): void {
    let command: CatalogCommand = new CatalogCommand(catalog);

    this.openingSource.next(command);

  }

  public executeOpenCommand(command: CatalogCommand): void {
    command.execute(catalog => {
      this.openedSource.next(catalog);
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
}
