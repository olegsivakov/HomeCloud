import { Component } from '@angular/core';

@Component({
	selector: 'nav-menu',
	templateUrl: './nav-menu.component.html',
	styleUrls: ['./../../assets/themes/blue.css', './nav-menu.component.css' ]
})
export class NavMenuComponent {
	public _opened: boolean = false;

	public _toggleSidebar() {
		this._opened = !this._opened;
	}
}