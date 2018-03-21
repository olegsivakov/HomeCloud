import { Component, Input, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';

import { Catalog } from '../../../models/catalog';

@Component({
  selector: 'app-catalog-edit',
  templateUrl: './catalog-edit.component.html',
  styleUrls: ['./catalog-edit.component.css']
})
export class CatalogEditComponent implements OnInit, OnDestroy {
  
  private _source: Catalog = null;
  private _catalog: Catalog = null;

  @Input('catalog')
  public set catalog(value: Catalog) {
    this._source = value;
    this._catalog = Object.create(this._source);
  }

  public get catalog(): Catalog {
    return this._catalog;
  }

  public get isVisible(): boolean {
    return this._source != null && this._catalog != null;
  }
  
  @Output('save')
  saveEmitter = new EventEmitter<Catalog>();

  @Output('cancel')
  cancelEmitter = new EventEmitter();

  constructor() { }

  private onSave() {
    this.saveEmitter.emit(this._catalog);
  }

  private onCancel() {
    this.cancelEmitter.emit();
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this._source = null;
    this._catalog = null;
  }
}
