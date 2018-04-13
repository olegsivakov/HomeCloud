import { NotificationType } from "./notification-type";

export class Notification {

    public messages: Array<string> = new Array<string>();

    public constructor(public type: NotificationType, public title: string, message: string) {
        this.messages.push(message);
    }
}