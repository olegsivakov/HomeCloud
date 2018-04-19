import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { ClarityModule } from 'clarity-angular';

import { AppComponent } from './app.component';
import { ROUTING } from './app.routes';

// -------------------- Layout --------------------
import { FooterComponent } from './components/footer/footer.component';
import { RightPanelComponent } from './components/shared/right-panel/right-panel.component';
import { NavigationComponent } from './components/navigation/navigation.component';

// -------------------- Route Components --------------------
import { CatalogContainerComponent } from './components/catalog-container/catalog-container.component';
import { CatalogBreadcrumbComponent } from './components/catalog/catalog-breadcrumb/catalog-breadcrumb.component';

// -------------------- Catalog Components --------------------
import { CatalogCardComponent } from './components//catalog/catalog-card/catalog-card.component';
import { CatalogDetailsComponent } from './components/catalog/catalog-details/catalog-details.component';
import { CatalogEditComponent } from './components/catalog/catalog-edit/catalog-edit.component';
import { CatalogRemoveComponent } from './components/catalog/catalog-remove/catalog-remove.component';

// -------------------- CatalogEntry Components --------------------
import { CatalogEntryCardComponent } from './components/catalog-entry/catalog-entry-card/catalog-entry-card.component';
import { CatalogEntryRemoveComponent } from './components/catalog-entry/catalog-entry-remove/catalog-entry-remove.component';
import { CatalogEntryDetailsComponent } from './components/catalog-entry/catalog-entry-details/catalog-entry-details.component';

// -------------------- Shared Components --------------------
import { ProgressComponent } from './components/shared/progress/progress.component';
import { NotificationComponent } from './components/shared/notification/notification.component';
import { AlertComponent } from './components/shared/alert/alert.component';

// -------------------- Services --------------------
import { RightPanelService } from './services/shared/right-panel/right-panel.service';
import { NotificationService } from './services/shared/notification/notification.service';
import { NotificationStateService } from './services/shared/notification-state/notification-state.service';
import { ProgressService } from './services/shared/progress/progress.service';

import { CloneableService } from './services/cloneable/cloneable.service';
import { ResourceService } from './services/resource/resource.service';

import { StorageService } from './services/storage/storage.service';
import { CatalogStateService } from './services/catalog-state/catalog-state.service';
import { CatalogService } from './services/catalog/catalog.service';
import { CatalogEntryService } from './services/catalog-entry/catalog-entry.service';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    NavigationComponent,

    RightPanelComponent,

    CatalogContainerComponent,
    CatalogBreadcrumbComponent,

    CatalogDetailsComponent,
    CatalogCardComponent,
    CatalogEditComponent,
    CatalogRemoveComponent,

    CatalogEntryCardComponent,
    CatalogEntryRemoveComponent,
    CatalogEntryDetailsComponent,

    NotificationComponent,
    AlertComponent,
    ProgressComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ROUTING,
    ClarityModule.forRoot()
  ],
  providers: [
    RightPanelService,

    NotificationService,
    NotificationStateService,
    ProgressService,

    CloneableService,
    ResourceService,

    StorageService,
    CatalogStateService,
    CatalogService,    
    CatalogEntryService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
