import { NotificationType } from "./notification-type";

export class Notification {

    public title: string = "";
    public message: string = "";
    public type: NotificationType = NotificationType.info;
    public timeExpired: Date;

    public constructor(type: NotificationType, title: string, message: string, timeExpired: Date) {
        this.type = type;
        this.title = title;
        this.message = message;

        this.timeExpired =timeExpired;
    }
}