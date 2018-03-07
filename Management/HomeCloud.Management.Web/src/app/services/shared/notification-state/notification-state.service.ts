import { Injectable } from '@angular/core';

import { NotificationState } from '../../../models/notification-state';
import { Notification } from '../../../models/notification';

import { NotificationService } from '../notification/notification.service';

const NotificationCleanupInterval: number = 3000;

@Injectable()
export class NotificationStateService {

  private states: Array<NotificationState> = new Array<NotificationState>();

  constructor(private notificationService: NotificationService) {
    setInterval(() => {
      this.cleanup();
    }, NotificationCleanupInterval);
  }

  public addNotification(notification: Notification): NotificationState {
    let state: NotificationState = new NotificationState(notification);

    this.states.push(state);

    return state;
  }

  private remove(state: NotificationState): void {
    let index = this.states.indexOf(state);
    if (index >= 0) {
      this.states.splice(index, 1);
    }
  }

  private cleanup() {
    for (let state of this.states) {
      if (state.isExpired) {
        this.remove(state);

        this.notificationService.remove(state.notification);
      }
    }
  }
}
