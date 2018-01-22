import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './components/app/app.component';
import { NotificationComponent } from './components/notification/notification.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { UserProfileBarComponent } from './components/user-profile-bar/user-profile-bar.component';
import { HomeComponent } from './components/home/home.component';
import { StorageListComponent } from './components/storage-list/storage-list.component';
import { DataComponent } from './components/data/data.component';

import { SidebarModule } from 'ng-sidebar';
import { ExpandableListModule } from 'angular2-expandable-list';
import { FileUploadModule } from 'ng2-file-upload';
import { ContextmenuModule } from 'ng2-contextmenu';
import { ToasterModule } from 'angular2-toaster';

@NgModule({
	declarations: [
		AppComponent,
		NotificationComponent,
		NavMenuComponent,
		UserProfileBarComponent,
		HomeComponent,
		StorageListComponent,
		DataComponent
	],
	imports: [
		CommonModule,
		HttpModule,
		FormsModule,
		NoopAnimationsModule,
		SidebarModule.forRoot(),
		ExpandableListModule,
		FileUploadModule,
		ContextmenuModule,
		ToasterModule,
		RouterModule.forRoot([
			{ path: '', redirectTo: 'home', pathMatch: 'full' },
			{ path: 'home', component: HomeComponent },
			{ path: 'catalogs', component: DataComponent },
			{ path: '**', redirectTo: 'home' }
		])
	],
	entryComponents: [
		NotificationComponent
	]
})
export class AppModuleShared {
}
