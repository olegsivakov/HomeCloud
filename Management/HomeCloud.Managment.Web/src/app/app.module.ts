import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { ClarityModule } from 'clarity-angular';

import { AppComponent } from './app.component';
import { ROUTING } from './app.routes';
import { FooterComponent } from './components/footer/footer.component';
import { NavigationComponent } from './components/navigation/navigation.component';
import { RightPanelService } from './components/shared/right-panel/right-panel.service';
import { RightPanelComponent } from './components/shared/right-panel/right-panel.component';

import { CatalogService } from './components/catalog/catalog.service';
import { CatalogComponent } from './components/catalog/catalog.component';
import { CatalogDetailsComponent } from './components/catalog-details/catalog-details.component';
import { CatalogListComponent } from './components/catalog-list/catalog-list.component';

import { FileComponent } from './components/file/file.component';
import { CatalogEditComponent } from './components/catalog-edit/catalog-edit.component';
import { CatalogRemoveComponent } from './components/catalog-remove/catalog-remove.component';

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

    CatalogRemoveComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ROUTING,
    ClarityModule.forRoot()
  ],
  providers: [
    RightPanelService,
    CatalogService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
