import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { CatalogEntry } from '../../../models/catalog-entry';

@Component({
  selector: 'app-catalog-entry-remove',
  templateUrl: './catalog-entry-remove.component.html',
  styleUrls: ['./catalog-entry-remove.component.css']
})
export class CatalogEntryRemoveComponent implements OnInit {

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

  @Output('remove')
  removeEmitter = new EventEmitter<CatalogEntry>();

  @Output('cancel')
  cancelEmitter = new EventEmitter();

  constructor() { }

  private onRemove() {
    this.removeEmitter.emit(this.entry);
    this.onCancel();
  }

  private onCancel() {
    this.cancelEmitter.emit();
    this.entry = null;
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.entry = null;
  }
}
