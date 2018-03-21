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

// -------------------- Catalog Components --------------------
import { CatalogBreadcrumbComponent } from './components/catalogs/catalog-breadcrumb/catalog-breadcrumb.component';
import { CatalogCardComponent } from './components//catalogs/catalog-card/catalog-card.component';
import { CatalogComponent } from './components/catalogs/catalog/catalog.component';
import { CatalogDetailsComponent } from './components/catalogs/catalog-details/catalog-details.component';
import { CatalogEditComponent } from './components/catalogs/catalog-edit/catalog-edit.component';
import { CatalogRemoveComponent } from './components/catalogs/catalog-remove/catalog-remove.component';
import { CatalogListComponent } from './components/catalogs/catalog-list/catalog-list.component';

// -------------------- File Components --------------------
import { FileComponent } from './components/file/file.component';

// -------------------- Shared Components --------------------
import { ProgressComponent } from './components/shared/progress/progress.component';
import { NotificationComponent } from './components/shared/notification/notification.component';
import { AlertComponent } from './components/shared/alert/alert.component';

// -------------------- Services --------------------
import { RightPanelService } from './services/shared/right-panel/right-panel.service';
import { NotificationService } from './services/shared/notification/notification.service';
import { NotificationStateService } from './services/shared/notification-state/notification-state.service';
import { ProgressService } from './services/shared/progress/progress.service';

import { CatalogService } from './services/catalog/catalog.service';
import { ResourceService } from './services/resource/resource.service';
import { CatalogDataService } from './services/catalog/catalog-data.service';

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

    NotificationComponent,
    AlertComponent,
    ProgressComponent,
    CatalogBreadcrumbComponent,
    CatalogCardComponent
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
    CatalogService,
    CatalogDataService,
    NotificationService,
    NotificationStateService,
    ProgressService,

    ResourceService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
