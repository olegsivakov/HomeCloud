import { NotificationType } from "./notification-type";

export class Notification {

    public constructor(public type: NotificationType, public title: string, public message: string) {
    }
}