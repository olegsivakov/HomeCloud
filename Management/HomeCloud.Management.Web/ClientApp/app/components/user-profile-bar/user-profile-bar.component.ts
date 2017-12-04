import { Component } from '@angular/core';

@Component({
	selector: 'user-profile-bar',
	templateUrl: './user-profile-bar.component.html',
	styleUrls: ['./user-profile-bar.component.css']
})
export class UserProfileBarComponent {
	public _opened: boolean = false;

	public _toggleSidebar() {
		this._opened = !this._opened;
	}
}