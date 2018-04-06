import { Component, OnInit, OnDestroy, EventEmitter, Output, Input } from '@angular/core';

import { Catalog } from '../../../models/catalog';

@Component({
  selector: 'app-catalog-edit',
  templateUrl: './catalog-edit.component.html',
  styleUrls: ['./catalog-edit.component.css']
})
export class CatalogEditComponent implements OnInit, OnDestroy {

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

  @Output('save')
  saveEmitter = new EventEmitter<Catalog>();

  @Output('cancel')
  cancelEmitter = new EventEmitter<Catalog>();

  constructor() { }

  private onSave() {
    this.saveEmitter.emit(this.catalog);

    this.onCancel();
  }

  private onCancel() {
    this.cancelEmitter.emit(this.catalog);
    this.catalog = null;
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.catalog = null;
  }
}
