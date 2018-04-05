import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Catalog } from '../../../models/catalog';
import { CatalogRelation } from '../../../models/catalog-relation';

@Component({
  selector: 'app-catalog-card',
  templateUrl: './catalog-card.component.html',
  styleUrls: ['./catalog-card.component.css']
})
export class CatalogCardComponent implements OnInit {

  @Input()
  public catalog: Catalog = null;

  @Output('load')
  loadEmitter = new EventEmitter<Catalog>();

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

  private get canLoad(): boolean {
    return !(this.catalog._links instanceof CatalogRelation);
  }

  private onLoad(): void {
    if (this.canLoad) {
      this.loadEmitter.emit(this.catalog);
    }
  }
  
  private get canOpen(): boolean {
    return this.catalog.isCatalog;
  }

  private onOpen(): void {
    if (this.canOpen) {
      this.openEmitter.emit(this.catalog);
    }
  }

  private get canDetail(): boolean {
    return true;
  }

  private onDetail(): void {
    if (this.canDetail) {
      this.detailEmitter.emit(this.catalog);
    }
  }

  private get canEdit(): boolean {
    return this.catalog.hasUpdate();
  }

  private onEdit(): void {
    if (this.canEdit) {
      this.editEmitter.emit(this.catalog);
    }
  }

  private get canRemove(): boolean {
    return this.catalog.hasDelete();
  }

  private onRemove(): void {
    if (this.canRemove) {
      this.removeEmitter.emit(this.catalog);
    }
  }
}
