import { Component } from '@angular/core';
import { ExpandableComponent } from './../shared/expandable.component';

@Component({
	selector: 'user-profile-bar',
	templateUrl: './user-profile-bar.component.html',
	styleUrls: ['./user-profile-bar.component.css']
})
export class UserProfileBarComponent extends ExpandableComponent {
}