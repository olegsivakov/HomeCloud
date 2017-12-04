import { Component } from '@angular/core';

@Component({
    selector: 'app',
	templateUrl: './app.component.html',
    styleUrls: [ './../../assets/themes/blue.css', './app.component.css' ]
})
export class AppComponent {
	public _opened: boolean = false;

	public _toggleSidebar() {
		this._opened = !this._opened;
	}
}
