import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';
import { Catalog } from '../../../models/catalog';

@Component({
  selector: 'app-catalog-details',
  templateUrl: './catalog-details.component.html',
  styleUrls: ['./catalog-details.component.css']
})
export class CatalogDetailsComponent implements OnInit, OnDestroy {

  private _isVisible: boolean = false;

  @Input('catalog')
  public catalog: Catalog = null;

  @Input('visible')
  public set isVisible(value: boolean) {
    this._isVisible = value;
  }
  public get isVisible(): boolean {
    return this._isVisible && this.catalog != null;
  }

  constructor() {
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.catalog = null;
  }
}
