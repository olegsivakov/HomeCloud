import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Catalog } from '../../../models/catalog';

@Component({
  selector: 'app-catalog-card',
  templateUrl: './catalog-card.component.html',
  styleUrls: ['./catalog-card.component.css']
})
export class CatalogCardComponent implements OnInit {

  @Input()
  public catalog: Catalog = null;

  @Output('open')
  openEmitter = new EventEmitter<Catalog>();

  @Output('detail')
  detailEmitter = new EventEmitter<Catalog>();

  @Output('edit')
  editEmitter = new EventEmitter<Catalog>();

  @Output('remove')
  removeEmitter = new EventEmitter<Catalog>();

  constructor() { }

  ngOnInit() {
  }

  private onOpen(): void {
    this.openEmitter.emit(this.catalog);
  }

  private onDetail(): void {
    this.detailEmitter.emit(this.catalog);
  }

  private onEdit(): void {
    this.editEmitter.emit(this.catalog);
  }

  private onRemove(): void {
    this.removeEmitter.emit(this.catalog);
  }
}
