import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';

import { Catalog } from '../../../models/catalog';
import { CloneableService } from '../../../services/cloneable/cloneable.service';

@Component({
  selector: 'app-catalog-remove',
  templateUrl: './catalog-remove.component.html',
  styleUrls: ['./catalog-remove.component.css']
})
export class CatalogRemoveComponent implements OnInit, OnDestroy {

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

  @Output('remove')
  removeEmitter = new EventEmitter<Catalog>();

  @Output('cancel')
  cancelEmitter = new EventEmitter();

  constructor(private cloneableService: CloneableService) { }

  private onRemove() {
    this.removeEmitter.emit(this.catalog);
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
