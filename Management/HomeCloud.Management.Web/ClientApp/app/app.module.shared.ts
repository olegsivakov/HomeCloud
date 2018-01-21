import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { UserProfileBarComponent } from './components/user-profile-bar/user-profile-bar.component';
import { HomeComponent } from './components/home/home.component';
import { StorageListComponent } from './components/storage-list/storage-list.component';
import { DataComponent } from './components/data/data.component';

import { SidebarModule } from 'ng-sidebar';
import { ExpandableListModule } from 'angular2-expandable-list';
import { FileUploadModule } from 'ng2-file-upload';
import { ContextmenuModule  } from 'ng2-contextmenu';

@NgModule({
    declarations: [
		AppComponent,
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
		SidebarModule.forRoot(),
		ExpandableListModule,
		FileUploadModule,
		ContextmenuModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
			{ path: 'home', component: HomeComponent },
			{ path: 'catalogs', component: DataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
