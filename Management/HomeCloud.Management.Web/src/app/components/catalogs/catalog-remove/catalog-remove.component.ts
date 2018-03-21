import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { Catalog } from '../../../models/catalog';

@Component({
  selector: 'app-catalog-remove',
  templateUrl: './catalog-remove.component.html',
  styleUrls: ['./catalog-remove.component.css']
})
export class CatalogRemoveComponent implements OnInit, OnDestroy {

  @Input('catalog')
  public catalog: Catalog = null;

  @Output('remove')
  removeEmitter = new EventEmitter<Catalog>();

  @Output('cancel')
  cancelEmitter = new EventEmitter();

  public get isvisible(): boolean {
    return this.catalog != null;
  }

  constructor() { }

  private onRemove() {
    this.removeEmitter.emit(this.catalog);
  }

  private onCancel() {
    this.cancelEmitter.emit();
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.catalog = null;
  }
}
