import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Notification } from '../../../models/notification';
import { NotificationService } from '../../../services/shared/notification/notification.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit, OnDestroy {

  private notificationCreatedSubscription: ISubscription = null;
  private notificationRemovedSubscription: ISubscription = null;

  public notifications: Array<Notification> = new Array<Notification>();

  constructor(private notificationService: NotificationService) { }

  ngOnInit() {
    this.notificationCreatedSubscription = this.notificationService.notificationCreated$.subscribe(notification => {
      this.notifications.push(notification);
    });

    this.notificationRemovedSubscription = this.notificationService.notificationRemoved$.subscribe(notification => {
      this.removeNotification(notification);
    });
  }

  ngOnDestroy(): void {
    if (this.notificationCreatedSubscription) {
      this.notificationCreatedSubscription.unsubscribe();
      this.notificationCreatedSubscription = null;
    }

    if (this.notificationRemovedSubscription) {
      this.notificationRemovedSubscription.unsubscribe();
      this.notificationRemovedSubscription = null;
    }
  }  

  private removeNotification(notification: Notification): void {
    let index = this.notifications.indexOf(notification);
    if (index >= 0) {
      this.notifications.splice(index, 1);
    }
  }
}
