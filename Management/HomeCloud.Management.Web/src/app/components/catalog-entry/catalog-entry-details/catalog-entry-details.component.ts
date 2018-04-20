import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { CatalogEntry } from '../../../models/catalog-entry';

@Component({
  selector: 'app-catalog-entry-details',
  templateUrl: './catalog-entry-details.component.html',
  styleUrls: ['./catalog-entry-details.component.css']
})
export class CatalogEntryDetailsComponent implements OnInit {

  @Input('entry')
  public entry: CatalogEntry = null;

  constructor() {
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.entry = null;
  }
}
