import { Component, Input, OnInit } from '@angular/core';

import { Catalog } from '../../models/catalog';
import { CatalogService } from './catalog.service';

@Component({  
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {

  @Input()
  public catalog: Catalog = null;

  constructor(private catalogService: CatalogService) {    
  }

  ngOnInit() {
  }

  public showDetails(): void {
    this.catalogService.requestDetails(this.catalog);
  }

  public edit(): void {
    this.catalogService.requestUpdate(this.catalog);
  }

  public remove(): void {
    this.catalogService.requestRemove(this.catalog);
  }
}
