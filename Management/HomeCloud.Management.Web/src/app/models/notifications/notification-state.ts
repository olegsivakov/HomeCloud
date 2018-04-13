import { Notification } from "./notification";
import { NotificationType } from "./notification-type";

const NotificationLifetime: number = 3000;

export class NotificationState {

    private timeExpired: Date = null;

    constructor(public notification: Notification) { }

    public get isExpired() {
        return this.timeExpired && this.timeExpired <= new Date();
    }

    public setWarning(reason?: string, description?: string | Array<string>): NotificationState {
        this.notification.type = NotificationType.warning;

        this.notification.title = reason ? reason : this.notification.title;
        if (description) {
            if (description instanceof Array) {
                this.notification.messages = description;
            }
            else {
                this.notification.messages.splice(0, this.notification.messages.length, description);
            }
        }

        return this;
    }

    public setSucceded(reason?: string, description?: string | Array<string>): NotificationState {
        this.notification.type = NotificationType.success;

        this.notification.title = reason ? reason : this.notification.title;
        if (description) {
            if (description instanceof Array) {
                this.notification.messages = description;
            }
            else {
                this.notification.messages.splice(0, this.notification.messages.length, description);
            }
        }

        return this;
    }

    public setFailed(reason?: string, description?: string | Array<string>): NotificationState {
        this.notification.type = NotificationType.error;

        this.notification.title = reason ? reason : this.notification.title;
        if (description) {
            if (description instanceof Array) {
                this.notification.messages = description;
            }
            else {
                this.notification.messages.splice(0, this.notification.messages.length, description);
            }
        }

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