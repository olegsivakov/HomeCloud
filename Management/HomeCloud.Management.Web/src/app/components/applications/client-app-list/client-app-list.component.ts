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
import { GrantService } from '../../../services/grant/grant.service';
import { ClientAppDetailsTab } from '../../../models/applications/client-app-details-tab';


@Component({
  selector: 'app-client-app-list',
  templateUrl: './client-app-list.component.html',
  styleUrls: ['./client-app-list.component.css']
})
export class ClientAppListComponent implements OnInit, OnDestroy {

  public selected: ClientApplication = null;
  public data: PagedArray<Application> = new PagedArray<Application>();
  public grantTypes: Array<GrantType> = new Array<GrantType>();

  private errors: Array<string> = new Array<string>();

  public isSaveMode: boolean = false;
  
  private listSubscription: ISubscription = null;
  private nextSubscription: ISubscription = null;
  private validateSubscription: ISubscription = null;
  private createApplicationSubscription: ISubscription = null;
  private getClientSubscription: ISubscription = null;
  private selfClientSubscription: ISubscription = null;
  private grantTypesSubscription: ISubscription = null;

  constructor(
    private progressService: ProgressService,    
    private notificationService: NotificationService,
    private notificationStateService: NotificationStateService,
    private clientApplicationService: ClientApplicationService,
    private grantservice: GrantService) {      
    }

  ngOnInit() {
    this.progressService.show();

    this.grantTypesSubscription = this.grantservice.listTypes().subscribe(data => {
      this.grantTypes = data;
    });

    this.listSubscription = this.clientApplicationService.list(20).subscribe(data => {
      this.data = data;

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

    if (this.getClientSubscription) {
      this.getClientSubscription.unsubscribe();
      this.getClientSubscription = null;
    }

    if (this.selfClientSubscription) {
      this.selfClientSubscription.unsubscribe();
      this.selfClientSubscription = null;
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
      let client: ClientApplication = application as ClientApplication;
      if (this.grantTypes.length > 0) {
        client.grantType = this.grantTypes[0].id;
      }

      this.validateSubscription = this.clientApplicationService.validate(client).subscribe(data => {
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

      let client: ClientApplication = application as ClientApplication;
      if (this.grantTypes.length > 0) {
        client.grantType = this.grantTypes[0].id;
      }

      this.createApplicationSubscription = this.clientApplicationService.create(client).subscribe(item => {
        if (item) {
          this.data.push(item);
          
          this.select(item);
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

  public load(application: Application);
  public load(application: Application, isSelected: boolean);
  public load(application: Application, isSelected?: boolean) {
    let client: ClientApplication = application as ClientApplication;

    this.getClientSubscription = this.clientApplicationService.get(client).subscribe(data => {
      let index: number = this.data.indexOf(application);
      if (index >= 0) {        
        this.data[index]._links = data._links;

        if (isSelected) {
          this.selected = data;
        }
      }
    });
  }
  public select(application: Application) {
    let client: ClientApplication = application as ClientApplication;

    if (client.hasGet && client.hasGet()) {
      this.load(application, true);
    }
    else {
      this.selected = client;
    }
  }

  public loadResources(tab: ClientAppDetailsTab) {
    switch(tab) {
      case ClientAppDetailsTab.essentials: {

        if (this.selected.hasSelf && this.selected.hasSelf()) {
          this.progressService.show();

          this.selfClientSubscription = this.clientApplicationService.self(this.selected).subscribe(item => {
            this.selected = item;

            this.progressService.hide();
          }, error => {
            this.progressService.hide();
          });
        }

        break;
      }
    }
  }

  public cancel() {
    if (!this.isSaveMode) {
      this.selected = null;
    }

    this.isSaveMode = false;
  }
}
