import {ModuleWithProviders} from "@angular/core/src/metadata/ng_module";
import {Routes, RouterModule}from '@angular/router';

import { CatalogContainerComponent } from "./components/catalog-container/catalog-container.component";


export const APP_ROUTES: Routes = [
  {
    path: '',
    redirectTo: '/',
    pathMatch: 'full',
  },
  {
    path: 'catalogs/:id',
    component: CatalogContainerComponent,
  }
];

export const ROUTING: ModuleWithProviders = RouterModule.forRoot(APP_ROUTES);