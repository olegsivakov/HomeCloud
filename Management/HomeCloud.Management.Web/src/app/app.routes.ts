import {ModuleWithProviders} from "@angular/core/src/metadata/ng_module";
import {Routes, RouterModule}from '@angular/router';

import { CatalogListComponent } from "./components/catalog-list/catalog-list.component";


export const APP_ROUTES: Routes = [
  {
    path: '',
    redirectTo: '/',
    pathMatch: 'full',
  },
  {
    path: 'catalogs',
    component: CatalogListComponent,
  }
];

export const ROUTING: ModuleWithProviders = RouterModule.forRoot(APP_ROUTES);