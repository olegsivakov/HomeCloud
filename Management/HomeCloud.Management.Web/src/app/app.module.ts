import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { ClarityModule } from 'clarity-angular';

import { AppComponent } from './app.component';
import { ROUTING } from './app.routes';

import { FooterComponent } from './components/footer/footer.component';
import { RightPanelComponent } from './components/shared/right-panel/right-panel.component';
import { NavigationComponent } from './components/navigation/navigation.component';

import { CatalogComponent } from './components/catalog/catalog.component';
import { CatalogDetailsComponent } from './components/catalog-details/catalog-details.component';
import { CatalogListComponent } from './components/catalog-list/catalog-list.component';

import { FileComponent } from './components/file/file.component';
import { CatalogEditComponent } from './components/catalog-edit/catalog-edit.component';
import { CatalogRemoveComponent } from './components/catalog-remove/catalog-remove.component';
import { NotificationComponent } from './components/shared/notification/notification.component';

import { RightPanelService } from './services/shared/right-panel/right-panel.service';
import { NotificationService } from './services/shared/notification/notification.service';
import { NotificationStateService } from './services/shared/notification-state/notification-state.service';

import { CatalogService } from './services/catalog/catalog.service';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    NavigationComponent,

    RightPanelComponent,

    CatalogListComponent,
    CatalogComponent,
    CatalogDetailsComponent,

    FileComponent,

    CatalogEditComponent,
    CatalogRemoveComponent,

    NotificationComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    ROUTING,
    ClarityModule.forRoot()
  ],
  providers: [
    RightPanelService,
    CatalogService,
    NotificationService,
    NotificationStateService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
