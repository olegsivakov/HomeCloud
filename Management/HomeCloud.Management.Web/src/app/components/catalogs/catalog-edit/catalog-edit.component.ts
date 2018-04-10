import { Component, OnInit, OnDestroy, EventEmitter, Output, Input } from '@angular/core';

import { Catalog } from '../../../models/catalog';

@Component({
  selector: 'app-catalog-edit',
  templateUrl: './catalog-edit.component.html',
  styleUrls: ['./catalog-edit.component.css']
})
export class CatalogEditComponent implements OnInit, OnDestroy {

  private _isVisible: boolean = false;
  private _catalog: Catalog = null;

  @Input('catalog')
  public set catalog(value: Catalog) {
    if (value) {
      if (!this._catalog) {
        this._catalog = new Catalog();
      }

      if (this._catalog.id != value.id) {
        this._catalog = Object.assign(this._catalog, value);
      }
    }
    else {
      this._catalog = value;
    }
  }

  public get catalog(): Catalog {
    return this._catalog;
  }

  @Input('visible')
  public set isVisible(value: boolean) {
    this._isVisible = value;
  }
  public get isVisible(): boolean {
    return this._isVisible && this.catalog != null;
  }

  @Output('save')
  saveEmitter = new EventEmitter<Catalog>();

  @Output('cancel')
  cancelEmitter = new EventEmitter();

  constructor() { }

  private onSave() {
    this.saveEmitter.emit(this.catalog);
    this.onCancel();
  }

  private onCancel() {
    this.cancelEmitter.emit();
    this.catalog = null;
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.catalog = null;
  }
}
