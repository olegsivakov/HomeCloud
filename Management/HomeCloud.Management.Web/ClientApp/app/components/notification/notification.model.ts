import { Toast, BodyOutputType } from 'angular2-toaster';
import { NotificationComponent } from './notification.component';

export class Notification implements Toast {

	public type: string;
	public notificationType: NotificationType = NotificationType.Success;
	public body: any = NotificationComponent;
	public bodyOutputType: BodyOutputType = BodyOutputType.Component;
	public subject: string;
	public message: string;

	constructor(type: NotificationType, subject: string, message: string) {
		this.notificationType = type;
		this.type = this.notificationType.toString();
		this.subject = subject;
		this.message = message;
	}
}

export enum NotificationType {
	Success = 0,
	Warning = 1,
	Error = 2,
	Info = 3
}
