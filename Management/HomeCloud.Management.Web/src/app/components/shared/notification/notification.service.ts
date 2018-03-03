import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Notification } from '../../../models/notification';
import { NotificationType } from '../../../models/notification-type';

const NotificationLifetime: number = 50000;

@Injectable()
export class NotificationService {

  private notificationCreatedSource = new Subject<Notification>();
  private notificationRemovingSource = new Subject<Date>();

  notificationCreated$ = this.notificationCreatedSource.asObservable();
  notificationRemoving$ = this.notificationRemovingSource.asObservable();

  constructor() {
    setInterval(() => {
      this.notificationRemovingSource.next();
    }, NotificationLifetime);
  }

  public info(title: string, message: string): void {
    let notification: Notification = new Notification(NotificationType.info, title, message, this.getDateExpired());

    this.notificationCreatedSource.next(notification);
  }

  public success(title: string, message: string): void {
    let notification: Notification = new Notification(NotificationType.success, title, message, this.getDateExpired());

    this.notificationCreatedSource.next(notification);
  }

  public warning(title: string, message: string): void {
    let notification: Notification = new Notification(NotificationType.warning, title, message, this.getDateExpired());

    this.notificationCreatedSource.next(notification);
  }

  public error(title: string, message: string): void {
    let notification: Notification = new Notification(NotificationType.error, title, message, this.getDateExpired());

    this.notificationCreatedSource.next(notification);
  }

  private getDateExpired(): Date {
    let date = new Date();
    date.setMilliseconds(date.getMilliseconds() + NotificationLifetime);

    return date;
  }
}
