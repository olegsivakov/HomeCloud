import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Notification } from '../../../models/notification';
import { NotificationService } from './notification.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit, OnDestroy {

  private notificationCreatedSubscription: ISubscription = null;
  private notificationRemovingSubscription: ISubscription = null;

  public notifications: Array<Notification> = new Array<Notification>();

  constructor(private notificationService: NotificationService) { }

  ngOnInit() {
    this.notificationCreatedSubscription = this.notificationService.notificationCreated$.subscribe(notification => {
      this.notifications.push(notification);
    });

    this.notificationRemovingSubscription = this.notificationService.notificationRemoving$.subscribe(() => {
      let date = new Date();

      for (let notification of this.notifications) {
        if (notification.timeExpired <= date) {
          this.removeNotification(notification);
        }
      }
    });
  }

  ngOnDestroy(): void {
    if (this.notificationCreatedSubscription) {
      this.notificationCreatedSubscription.unsubscribe();
      this.notificationCreatedSubscription = null;
    }

    if (this.notificationRemovingSubscription) {
      this.notificationRemovingSubscription.unsubscribe();
      this.notificationRemovingSubscription = null;
    }
  }  

  private removeNotification(notification: Notification): boolean {
    let index = this.notifications.indexOf(notification);
    if (index >= 0) {
      this.notifications.splice(index, 1);
      
      return true;
    }

    return false;
  }
}
