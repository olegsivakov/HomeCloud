import { Notification } from "./notification";
import { NotificationType } from "./notification-type";

const NotificationLifetime: number = 3000;

export class NotificationState {

    private timeExpired: Date = null;

    constructor(public notification: Notification) { }

    public get isExpired() {
        return this.timeExpired && this.timeExpired <= new Date();
    }

    public setSucceded(reason?: string, description?: string): NotificationState {
        this.notification.type = NotificationType.success;

        this.notification.title = reason ? reason : this.notification.title;
        this.notification.message = description ? description : this.notification.message;

        return this;
    }

    public setFailed(reason?: string, description?: string): NotificationState {
        this.notification.type = NotificationType.error;

        this.notification.title = reason ? reason : this.notification.title;
        this.notification.message = description ? description : this.notification.message;

        return this;
    }
    public setExpired(timeout?: number): NotificationState {
        if (!this.timeExpired) {
            let date = new Date();
            date.setMilliseconds(date.getMilliseconds() + (timeout == null ? NotificationLifetime : timeout));

            this.timeExpired = date;
        }

        return this;
    }
}