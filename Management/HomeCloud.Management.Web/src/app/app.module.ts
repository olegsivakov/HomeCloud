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
import { DetailViewComponent } from './components/shared/detail-view/detail-view.component';
import { NavigationComponent } from './components/navigation/navigation.component';

// -------------------- Route Components --------------------
import { ApplicationContainerComponent } from './components/applications/application-container/application-container.component';
import { CatalogContainerComponent } from './components/catalog-container/catalog-container.component';
import { CatalogBreadcrumbComponent } from './components/catalog/catalog-breadcrumb/catalog-breadcrumb.component';

// -------------------- Client App Components --------------------
import { ClientAppDetailsComponent } from './components/applications/client-app-details/client-app-details.component';

// -------------------- Api App Components --------------------
import { ApiAppDetailsComponent } from './components/applications/api-app-details/api-app-details.component';

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
import { NotificationService } from './services/shared/notification/notification.service';
import { NotificationStateService } from './services/shared/notification-state/notification-state.service';
import { ProgressService } from './services/shared/progress/progress.service';

import { CloneableService } from './services/cloneable/cloneable.service';
import { ResourceService } from './services/resource/resource.service';

import { StorageService } from './services/storage/storage.service';
import { CatalogStateService } from './services/catalog-state/catalog-state.service';
import { CatalogService } from './services/catalog/catalog.service';
import { CatalogEntryService } from './services/catalog-entry/catalog-entry.service';

import { ApplicationCardComponent } from './components/applications/application-card/application-card.component';
import { ClientAppListComponent } from './components/applications/client-app-list/client-app-list.component';
import { ApiAppListComponent } from './components/applications/api-app-list/api-app-list.component';

import { ClientApplicationService } from './services/client-application/client-application.service';
import { ApplicationCreateComponent } from './components/applications/application-create/application-create.component';
import { GrantService } from './services/grant/grant.service';
import { ClientAppDetailsEssentialsComponent } from './components/applications/client-app-details-essentials/client-app-details-essentials.component';
import { ClientAppDetailsOriginsComponent } from './components/applications/client-app-details-origins/client-app-details-origins.component';
import { ClientAppDetailsSecretsComponent } from './components/applications/client-app-details-secrets/client-app-details-secrets.component';
import { ClientAppDetailsScopesComponent } from './components/applications/client-app-details-scopes/client-app-details-scopes.component';
import { GrantTypeExactPipe } from './pipes/grant-type-exact/grant-type-exact.pipe';
import { EmptyPipe } from './pipes/empty/empty.pipe';
import { ClientAppEditEssentialsComponent } from './components/applications/client-app-edit-essentials/client-app-edit-essentials.component';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    NavigationComponent,

    DetailViewComponent,

    ApplicationContainerComponent,

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
    ApplicationContainerComponent,
    ClientAppDetailsComponent,
    ApiAppDetailsComponent,
    ApplicationCardComponent,
    ClientAppListComponent,
    ApiAppListComponent,
    ApplicationCreateComponent,
    ClientAppDetailsEssentialsComponent,
    ClientAppDetailsOriginsComponent,
    ClientAppDetailsSecretsComponent,
    ClientAppDetailsScopesComponent,
    GrantTypeExactPipe,
    EmptyPipe,
    ClientAppEditEssentialsComponent
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

    NotificationService,
    NotificationStateService,
    ProgressService,

    CloneableService,
    ResourceService,

    StorageService,
    CatalogStateService,
    CatalogService,    
    CatalogEntryService,

    ClientApplicationService,
    GrantService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
