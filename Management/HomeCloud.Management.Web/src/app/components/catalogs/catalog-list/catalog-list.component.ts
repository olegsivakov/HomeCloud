import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { PagedArray } from '../../../models/paged-array';
import { StorageData } from '../../../models/storage-data';
import { Catalog } from '../../../models/catalog';
import { CatalogEntry } from '../../../models/catalog-entry';

import { Notification } from '../../../models/notifications/notification';
import { NotificationState } from '../../../models/notifications/notification-state';
import { HttpError } from '../../../models/http/http-error';

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
  private errors: Array<string> = new Array<string>();

  private data: PagedArray<StorageData> = new PagedArray<StorageData>();

  private catalogChangedSubscription: ISubscription = null;
  private listSubscription: ISubscription = null;
  private createCatalogSubscription: ISubscription = null;
  private createFileSubscription: ISubscription = null;
  private getCatalogSubscription: ISubscription = null;
  private validateSubscription: ISubscription = null;

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

  private get canSaveFile(): boolean {
    return this.catalogService.hasCreateFile();
  }
  private onSaveFile() {    
  }
  private validateFile(file: File) {
  }
  private saveFile(files: FileList) {
    if (this.canSaveFile && files.length > 0) {
      let entry: CatalogEntry = new CatalogEntry(files.item(0));

      let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to upload file '" + entry.name + "'.");
      let state: NotificationState = this.notificationStateService.addNotification(notification);

      this.createFileSubscription = this.catalogService.createFile(entry).subscribe(entry => {
        this.data.unshift(entry);
        state.setSucceded("Operation complete", "File '" + entry.name + "' has been upl successfully.").setExpired();
      }, (error: HttpError) => {
        if (error.statusCode == 500) {
          state.setFailed("Operation failure", "An error occured while uploading file.").setExpired();
        }
        else if (error.statusCode == 409) {
          state.setWarning("Operation failure", error.messages).setExpired();
        }
        else {
          state.setFailed("Operation failure", error.messages).setExpired();
        }
      }, () => {
        for(let index = 0; index < files.length; index++) {
          files.item(index).slice();
        }
      });
    }
  }


  private get isSaveCatalogMode(): boolean {
    return this.newCatalog != null;
  }  
  private get canSaveCatalog(): boolean {
    return this.catalogService.hasCreateCatalog();
  }
  private onSaveCatalog() {
    if (this.canSaveCatalog) {
      this.newCatalog = new Catalog();
    }
  }
  private validateCatalog(catalog: Catalog) {
    if (catalog instanceof Catalog && this.catalogService.hasValidate()) {
      this.validateSubscription = this.catalogService.validate(catalog).subscribe(data => {
        this.errors.splice(0, this.errors.length);
      }, (error: HttpError) => {
        if (error.statusCode != 500) {
          this.errors = error.messages;
        }
      });
    }
  }
  private saveCatalog(catalog: Catalog) {
    if (this.canSaveCatalog) {
      let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to create catalog '" + catalog.name + "'.");
      let state: NotificationState = this.notificationStateService.addNotification(notification);

      this.createCatalogSubscription = this.catalogService.createCatalog(catalog).subscribe(catalog => {
        this.data.unshift(catalog);
        state.setSucceded("Operation complete", "Catalog '" + catalog.name + "' has been created successfully.").setExpired();
      }, (error: HttpError) => {
          if (error.statusCode == 500) {
            state.setFailed("Operation failure", "An error occured while creating catalog.").setExpired();
          }
          else if (error.statusCode == 409) {
            state.setWarning("Operation failure", error.messages).setExpired();
          }
          else {
            state.setFailed("Operation failure", error.messages).setExpired();
          }
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

  private cancel() {
    if (this.isSaveCatalogMode) {
      this.newCatalog = null;
      this.errors.splice(0, this.errors.length);
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

    if (this.getCatalogSubscription) {
      this.getCatalogSubscription.unsubscribe();
      this.getCatalogSubscription = null;
    }

    if (this.validateSubscription) {
      this.validateSubscription.unsubscribe();
      this.validateSubscription = null;
    }

    if (this.createCatalogSubscription) {
      this.createCatalogSubscription.unsubscribe();
      this.createCatalogSubscription = null;
    }

    if (this.createFileSubscription) {
      this.createFileSubscription.unsubscribe();
      this.createFileSubscription = null;
    }

    this.data.splice(0);
    this.data = null;
  }
}
