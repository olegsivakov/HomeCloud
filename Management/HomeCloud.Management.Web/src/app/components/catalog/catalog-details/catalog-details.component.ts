import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { Catalog } from '../../../models/catalog';

@Component({
  selector: 'app-catalog-details',
  templateUrl: './catalog-details.component.html',
  styleUrls: ['./catalog-details.component.css']
})
export class CatalogDetailsComponent implements OnInit, OnDestroy {

  @Input('catalog')
  public catalog: Catalog = null;
  
  constructor() {
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.catalog = null;
  }
}
