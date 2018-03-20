import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Catalog } from '../../models/catalog';
import { ResourceService } from '../resource/resource.service';

const url: string = "http://localhost/catalogs/";

@Injectable()
export class CatalogDataService extends HttpService<Catalog> {

  constructor(protected resourceService: ResourceService) {    
    super(resourceService, url);
   }
}
