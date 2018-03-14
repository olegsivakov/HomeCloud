import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { StorageData } from '../../models/storage-data';
import { Catalog } from '../../models/catalog';

import { CatalogService } from '../../services/catalog/catalog.service';

@Component({
  selector: 'app-catalog-list',
  templateUrl: './catalog-list.component.html',
  styleUrls: ['./catalog-list.component.css']
})
export class CatalogListComponent implements OnInit, OnDestroy {

  private catalog: Catalog = null;

  private openingSubscription: ISubscription = null;
  private openedSubscription: ISubscription = null;
  private savedSubscription: ISubscription = null;
  private removedSubscription: ISubscription = null;

  private data: Array<StorageData> = new Array<StorageData>();

  constructor(
    private catalogService: CatalogService) { }

  ngOnInit() {
    this.openingSubscription = this.catalogService.opening$.subscribe(command => {
      this.catalog = command.catalog;
      this.data.splice(0, this.data.length);
      
      this.catalogService.executeOpenCommand(command);
    });

    this.openedSubscription = this.catalogService.opened$.subscribe(data => {
      this.data = data;
    });

    this.savedSubscription = this.catalogService.saved$.subscribe(catalog => {
      let item: StorageData = this.data.find(item => item.IsCatalog && item.ID == catalog.ID);

      let index: number = this.data.indexOf(item);
      if (index >= 0) {
        this.data.splice(index, 1, catalog);
      }
    });

    this.removedSubscription = this.catalogService.removed$.subscribe(catalog => {
      let item: StorageData = this.data.find(item => item.IsCatalog && item.ID == catalog.ID);

      let index: number = this.data.indexOf(item);
      if (index >= 0) {
        this.data.splice(index, 1);
      }
    });

    this.catalogService.createOpenCommand(null);
  }

  ngOnDestroy(): void {
    if (this.savedSubscription) {
      this.savedSubscription.unsubscribe();
      this.savedSubscription = null;
    }

    if (this.removedSubscription) {
      this.removedSubscription.unsubscribe();
      this.removedSubscription = null;
    }

    this.data.splice(0);
  }
}
