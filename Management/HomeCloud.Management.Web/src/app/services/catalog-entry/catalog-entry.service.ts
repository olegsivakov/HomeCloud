import { Injectable } from '@angular/core';

import { CatalogEntry } from '../../models/catalog-entry';

import { HttpService } from '../http/http.service';
import { ResourceService } from '../resource/resource.service';

@Injectable()
export class CatalogEntryService extends HttpService<CatalogEntry> {

  constructor(protected resourceService: ResourceService) {    
    super(CatalogEntry, resourceService);
  }
}
