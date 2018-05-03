import { Component, OnInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { PagedArray } from '../../models/paged-array';
import { StorageData } from '../../models/storage-data';
import { Catalog } from '../../models/catalog';
import { CatalogEntry } from '../../models/catalog-entry';

import { Notification } from '../../models/notifications/notification';
import { NotificationState } from '../../models/notifications/notification-state';
import { HttpError } from '../../models/http/http-error';

import { CatalogService } from '../../services/catalog/catalog.service';
import { CatalogEntryService } from '../../services/catalog-entry/catalog-entry.service';
import { CatalogStateService } from '../../services/catalog-state/catalog-state.service';
import { ProgressService } from '../../services/shared/progress/progress.service';
import { NotificationService } from '../../services/shared/notification/notification.service';
import { NotificationStateService } from '../../services/shared/notification-state/notification-state.service';

@Component({
  selector: 'app-catalog-container',
  templateUrl: './catalog-container.component.html',
  styleUrls: ['./catalog-container.component.css']
})
export class CatalogContainerComponent implements OnInit, OnDestroy {

  private newCatalog: Catalog = null;
  private details: StorageData = null;

  private errors: Array<string> = new Array<string>();

  private data: PagedArray<StorageData> = new PagedArray<StorageData>();

  private catalogChangedSubscription: ISubscription = null;
  private listSubscription: ISubscription = null;
  private nextSubscription: ISubscription = null;
  private createCatalogSubscription: ISubscription = null;
  private createFileSubscription: ISubscription = null;
  private getCatalogSubscription: ISubscription = null;
  private validateSubscription: ISubscription = null;

  @ViewChild('fileUpload') fileUpload: ElementRef;

  constructor(
    private progressService: ProgressService,
    private catalogStateService: CatalogStateService,
    private catalogService: CatalogService,
    private catalogEntryService: CatalogEntryService,
    private notificationService: NotificationService,
    private notificationStateService: NotificationStateService) {
      this.catalogChangedSubscription = this.catalogStateService.catalogChanged$.subscribe(catalog => {
        this.data.splice(0);
        this.cancel();
        this.open();
      });
    }

  ngOnInit() {
  }

  private open() {
    this.progressService.show();

    this.listSubscription = this.catalogService.list().subscribe(data => {
      this.data = data;

      this.progressService.hide();
    }, error => {
      this.progressService.hide();
    });
  }

  private get canNext(): boolean {
    return this.catalogService.hasNext();
  }
  private next() {
    if (this.canNext) {
      this.progressService.show();

      this.nextSubscription = this.catalogService.next().subscribe(data => {
        data.forEach(item => this.data.push(item));

        this.progressService.hide();
      }, error => {
        this.progressService.hide();
      });
    }
  }

  private get canSaveFile(): boolean {
    return this.catalogEntryService.hasCreate();
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

      this.createFileSubscription = this.catalogEntryService.create(entry).subscribe(entry => {
        if (entry) {
          this.data.unshift(entry);
        }

        this.fileUpload.nativeElement.value = "";

        state.setSucceded("Operation complete", "File" + (entry ? (" '" + entry.name + "'") : "") + " has been uploaded successfully.").setExpired();
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

        this.fileUpload.nativeElement.value = "";
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
    return this.catalogService.hasCreate();
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

      this.createCatalogSubscription = this.catalogService.create(catalog).subscribe(catalog => {
        if (catalog) {
          this.data.unshift(catalog);
        }

        state.setSucceded("Operation complete", "Catalog" + (catalog ? (" '" + catalog.name + "'") : "") + " has been created successfully.").setExpired();
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

  private save(data: StorageData) {
    if (this.details && this.details.isCatalog == data.isCatalog && this.details.id == data.id) {
      this.details = data;
    }
  }

  private remove(data: StorageData) {
    if (data) {
      let item: StorageData = this.data.find(item => item.id == data.id);

      let index: number = this.data.indexOf(item);
      if (index >= 0) {
        this.data.splice(index, 1);
      }

      if (this.details && this.details.isCatalog == data.isCatalog && this.details.id == data.id) {
        this.cancel();
      }
    }
  }

  private show(data: StorageData) {
    this.details = data;
  }

  private cancel() {
    if (this.isSaveCatalogMode) {
      this.newCatalog = null;
      this.errors.splice(0, this.errors.length);
    }

    this.details = null;
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

    if (this.nextSubscription) {
      this.nextSubscription.unsubscribe();
      this.nextSubscription = null;
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
