import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { CatalogEntry } from '../../../models/catalog-entry';

@Component({
  selector: 'app-catalog-entry-details',
  templateUrl: './catalog-entry-details.component.html',
  styleUrls: ['./catalog-entry-details.component.css']
})
export class CatalogEntryDetailsComponent implements OnInit {

  private _isVisible: boolean = false;

  @Input('entry')
  public entry: CatalogEntry = null;

  @Input('visible')
  public set isVisible(value: boolean) {
    this._isVisible = value;
  }
  public get isVisible(): boolean {
    return this._isVisible && this.entry != null;
  }

  constructor() {
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.entry = null;
  }
}
