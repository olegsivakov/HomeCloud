import { Component } from '@angular/core';
import { ExpandableComponent } from './../shared/expandable.component';

@Component({
	selector: 'nav-menu',
	templateUrl: './nav-menu.component.html',
	styleUrls: ['./../../assets/themes/blue.css', './nav-menu.component.css']
})
export class NavMenuComponent extends ExpandableComponent {
}
