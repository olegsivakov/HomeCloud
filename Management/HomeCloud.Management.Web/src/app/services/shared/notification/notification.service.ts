import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Notification } from '../../../models/notification';
import { NotificationType } from '../../../models/notification-type';

@Injectable()
export class NotificationService {

  private notificationCreatedSource = new Subject<Notification>();
  private notificationRemovedSource = new Subject<Notification>();

  notificationCreated$ = this.notificationCreatedSource.asObservable();
  notificationRemoved$ = this.notificationRemovedSource.asObservable();

  constructor() { }

  public info(title: string, message: string): Notification {
    let notification: Notification = new Notification(NotificationType.info, title, message);

    this.notificationCreatedSource.next(notification);

    return notification;
  }

  public progress(title: string, message: string): Notification {
    let notification: Notification = new Notification(NotificationType.progress, title, message);

    this.notificationCreatedSource.next(notification);

    return notification;
  }

  public success(title: string, message: string): Notification {
    let notification: Notification = new Notification(NotificationType.success, title, message);

    this.notificationCreatedSource.next(notification);

    return notification;
  }

  public warning(title: string, message: string): Notification {
    let notification: Notification = new Notification(NotificationType.warning, title, message);

    this.notificationCreatedSource.next(notification);

    return notification;
  }

  public error(title: string, message: string): Notification {
    let notification: Notification = new Notification(NotificationType.error, title, message);

    this.notificationCreatedSource.next(notification);

    return notification;
  }

  public remove(notification: Notification) {
    this.notificationRemovedSource.next(notification);
  }
}
