import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Application } from '../../../models/applications/application';
import { ClientApplication } from '../../../models/applications/client-application';

import { PagedArray } from '../../../models/paged-array';
import { HttpError } from '../../../models/http/http-error';

import { NotificationState } from '../../../models/notifications/notification-state';
import { Notification } from '../../../models/notifications/notification';

import { ProgressService } from '../../../services/shared/progress/progress.service';
import { NotificationService } from '../../../services/shared/notification/notification.service';
import { NotificationStateService } from '../../../services/shared/notification-state/notification-state.service';
import { ClientApplicationService } from '../../../services/client-application/client-application.service';
import { GrantType } from '../../../models/grants/grant-type';


@Component({
  selector: 'app-client-app-list',
  templateUrl: './client-app-list.component.html',
  styleUrls: ['./client-app-list.component.css']
})
export class ClientAppListComponent implements OnInit, OnDestroy {

  public selected: ClientApplication = null;
  public data: PagedArray<Application> = new PagedArray<Application>();
  public grantTypes: PagedArray<GrantType> = new PagedArray<GrantType>();

  private errors: Array<string> = new Array<string>();

  public isSaveMode: boolean = false;
  
  private listSubscription: ISubscription = null;
  private nextSubscription: ISubscription = null;
  private validateSubscription: ISubscription = null;
  private createApplicationSubscription: ISubscription = null;
  private grantTypesSubscription: ISubscription = null;

  constructor(
    private progressService: ProgressService,    
    private notificationService: NotificationService,
    private notificationStateService: NotificationStateService,
    private clientApplicationService: ClientApplicationService) { }

  ngOnInit() {
    this.progressService.show();

    this.listSubscription = this.clientApplicationService.list(20).subscribe(data => {
      this.data = data;

      if (this.clientApplicationService.hasGrantTypes()) {
        this.grantTypesSubscription = this.clientApplicationService.grantTypes().subscribe(data => this.grantTypes = data);
      }

      this.progressService.hide();
    }, error => {
      this.progressService.hide();
    });
  }

  ngOnDestroy(): void {
    if (this.listSubscription) {
      this.listSubscription.unsubscribe();
      this.listSubscription = null;
    }

    if (this.nextSubscription) {
      this.nextSubscription.unsubscribe();
      this.nextSubscription = null;
    }

    if (this.validateSubscription) {
      this.validateSubscription.unsubscribe();
      this.validateSubscription = null;
    }

    if (this.createApplicationSubscription) {
      this.createApplicationSubscription.unsubscribe();
      this.createApplicationSubscription = null;
    }

    if (this.grantTypesSubscription) {
      this.grantTypesSubscription.unsubscribe();
      this.grantTypesSubscription = null;
    }
  }

  public get canNext(): boolean {
    return this.clientApplicationService.hasNext();
  }
  public next() {
    if (this.canNext) {
      this.progressService.show();

      this.nextSubscription = this.clientApplicationService.next().subscribe(data => {
        data.forEach(item => this.data.push(item));

        this.progressService.hide();
      }, error => {
        this.progressService.hide();
      });
    }
  }

  public get canSave(): boolean {
    return this.clientApplicationService.hasCreate();
  }
  private onSave() {
    if (this.canSave) {
      this.isSaveMode = true;
    }
  }
  private validate(application: Application) {
    if (application instanceof Application && this.clientApplicationService.hasValidate()) {
      this.validateSubscription = this.clientApplicationService.validate(application as ClientApplication).subscribe(data => {
        this.errors.splice(0, this.errors.length);
      }, (error: HttpError) => {
        if (error.statusCode != 500) {
          this.errors = error.messages;
        }
      });
    }
  }
  private save(application: Application) {
    if (this.canSave) {
      let notification: Notification = this.notificationService.progress("Processing operation...", "Attempting to create application '" + application.name + "'.");
      let state: NotificationState = this.notificationStateService.addNotification(notification);

      this.createApplicationSubscription = this.clientApplicationService.create(application as ClientApplication).subscribe(item => {
        if (item) {
          this.data.push(item);
        }

        state.setSucceded("Operation complete", "Application" + (item ? (" '" + item.name + "'") : "") + " has been created successfully.").setExpired();
      }, (error: HttpError) => {
          if (error.statusCode == 500) {
            state.setFailed("Operation failure", "An error occured while creating application.").setExpired();
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

  public select(application: ClientApplication) {
    this.selected = application;
  }

  public cancel() {
    this.selected = null;
    this.isSaveMode = false;
  }
}
