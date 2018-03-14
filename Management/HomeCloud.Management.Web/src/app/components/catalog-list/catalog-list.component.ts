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

  private savedSubscription: ISubscription = null;
  private removedSubscription: ISubscription = null;

  private data: Array<StorageData> = new Array<StorageData>();

  constructor(private catalogService: CatalogService) {    
  }

  ngOnInit() {
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

    this.Initialize();
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

  private Initialize(): void {
    this.catalog = new Catalog();

    this.catalog.ID = "0";
    this.catalog.Name = "Parent Catalog";

    let data1: Catalog = new Catalog();
    data1.ID = "1";
    data1.Name = "Catalog 1";
    data1.CreationDate = new Date();
    data1.Size = "15Mb";

    this.data.push(data1);

    let data2: Catalog = new Catalog();
    data2.ID = "1";
    data2.Name = "Catalog 2";
    data2.CreationDate = new Date();
    data2.Size = "20Mb";

    this.data.push(data2);
  }
}
