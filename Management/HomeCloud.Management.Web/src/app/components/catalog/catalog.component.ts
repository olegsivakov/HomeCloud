import { Component, Input, OnInit } from '@angular/core';

import { Catalog } from '../../models/catalog';
import { CatalogService } from '../../services/catalog/catalog.service';

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

  public detail(): void {
    this.catalogService.createExpandCommand(this.catalog);
  }

  public edit(): void {
    this.catalogService.createSaveCommand(Object.assign({}, this.catalog));
  }

  public remove(): void {
    this.catalogService.createRemoveCommand(this.catalog);
  }
}
