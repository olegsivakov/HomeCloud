import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { Catalog } from '../../../models/catalog';
import { CatalogRelation } from '../../../models/catalog-relation';
import { CatalogState } from '../../../models/catalog-state';

import { CatalogService } from '../../../services/catalog/catalog.service';
import { RightPanelService } from '../../../services/shared/right-panel/right-panel.service';

@Component({
  selector: 'app-catalog-card',
  templateUrl: './catalog-card.component.html',
  styleUrls: ['./catalog-card.component.css']
})
export class CatalogCardComponent implements OnInit, OnDestroy {

  private state: CatalogState = CatalogState.view;

  private selfSubscription: ISubscription = null;
  private rightPanelVisibilityChangedSubscription: ISubscription = null;

  @Input('catalog')
  public catalog: Catalog = null;

  @Output('click')
  clickEmitter = new EventEmitter<Catalog>();

  @Output('save')
  saveEmitter = new EventEmitter<Catalog>();

  @Output('remove')
  removeEmitter = new EventEmitter<Catalog>();

  constructor(
    private catalogService: CatalogService,
    private rightPanelService: RightPanelService) {
      this.rightPanelVisibilityChangedSubscription = this.rightPanelService.visibilityChanged$.subscribe(isVisible => {
        if (!isVisible) {
          this.cancel();
        }
      });
    }

  private get canLoad(): boolean {
    return this.catalog.hasSelf && this.catalog.hasSelf() && !(this.catalog._links instanceof CatalogRelation);
  }
  private onLoad(): void {
    if (this.canLoad) {
      this.load();
    }
  }
  private load() {
    this.selfSubscription = this.catalogService.self(this.catalog).subscribe(data => {
      this.catalog = data;
    });
  }

  public get canClick(): boolean {
    return this.catalog.hasData && this.catalog.hasData();
  }
  public onClick(): void {
    if (this.canClick) {
      this.clickEmitter.emit(this.catalog);
    }
  }

  private get isDetailMode(): boolean {
    return this.state == CatalogState.details;
  }
  private get canDetail(): boolean {
    return this.catalog.hasSelf && this.catalog.hasSelf();
  }
  private onDetail(): void {
    this.state = CatalogState.details;
    this.rightPanelService.show();
  }

  private get isEditMode(): boolean {
    return this.state == CatalogState.edit;
  }
  private get canEdit(): boolean {
    return this.catalog.hasUpdate && this.catalog.hasUpdate();
  }
  private onEdit(): void {
    this.cancel(true);
    this.state = CatalogState.edit;
  }
  private save(catalog: Catalog): void {
    if (this.canEdit) {
      this.saveEmitter.emit(catalog);
    }
  }

  private get isRemoveMode(): boolean {
    return this.state == CatalogState.remove;
  }
  private get canRemove(): boolean {
    return this.catalog.hasDelete && this.catalog.hasDelete();
  }
  private onRemove(): void {
    this.cancel(true);
    this.state = CatalogState.remove;
  }
  private remove(catalog: Catalog): void {
    if (this.canRemove) {
      this.removeEmitter.emit(catalog);
    }
  }

  private cancel(forceDetailsCancel?: boolean): void {
    if (forceDetailsCancel && this.state == CatalogState.details) {
      this.rightPanelService.hide();
    }
    else {
      this.state = CatalogState.view;
    }
  }  

  ngOnInit() {
  }

  ngOnDestroy(): void {
    if (this.rightPanelVisibilityChangedSubscription) {
      this.rightPanelVisibilityChangedSubscription.unsubscribe();
      this.rightPanelVisibilityChangedSubscription = null;
    }

    if (this.selfSubscription) {
      this.selfSubscription.unsubscribe();
      this.selfSubscription = null;
    }

    this.catalog = null;
  }
}
