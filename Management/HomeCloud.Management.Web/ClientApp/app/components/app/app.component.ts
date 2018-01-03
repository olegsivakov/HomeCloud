import { Component } from '@angular/core';

@Component({
    selector: 'app',
	templateUrl: './app.component.html',
    styleUrls: [ './app.component.css' ]
})
export class AppComponent {
	public sideBarOpened: boolean = false;
	public userBarOpened: boolean = false;

	public toggleSideBar() {
		this.sideBarOpened = !this.sideBarOpened;
		this.userBarOpened = false;
	}

	public toggleUserBar() {
		this.userBarOpened = !this.userBarOpened;
		this.sideBarOpened = false;
	}
}
