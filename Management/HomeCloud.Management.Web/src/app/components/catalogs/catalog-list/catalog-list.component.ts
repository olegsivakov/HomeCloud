import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { StorageData } from '../../../models/storage-data';
import { Catalog } from '../../../models/catalog';

import { CatalogService } from '../../../services/catalog/catalog.service';

@Component({
  selector: 'app-catalog-list',
  templateUrl: './catalog-list.component.html',
  styleUrls: ['./catalog-list.component.css']
})
export class CatalogListComponent implements OnInit, OnDestroy {

  private catalog: Catalog = null;
  private selected: Catalog = null;
  private data: Array<StorageData> = new Array<StorageData>();

  private canEdit: boolean =false;
  private canRemove: boolean =false;

  constructor(
    ) { }    

  ngOnInit() {
    this.catalog = new Catalog();
    this.catalog.ID = "0";
    this.catalog.Name = "Root";
    this.data = this.Initialize(this.catalog);
  }

  public load(catalog?: Catalog) {
    
  }

  public showDetail(catalog: Catalog) {
  }

  public edit(catalog: Catalog) {
    this.selected = catalog;
    this.canEdit = true;
  }

  public save(catalog: Catalog) {
    alert("Saved");
    this.cancel(catalog);
  }

  public cancel(catalog: Catalog) {
    this.selected = null;
    this.canEdit = false;
    this.canRemove = false;
  }

  public showRemove(catalog: Catalog) {
  }

  ngOnDestroy(): void {
    this.data.splice(0);
  }

  private Initialize(parent: Catalog): Array<StorageData> {
    let data: Array<StorageData> = new Array<StorageData>();

    if (parent.ID == "0") {

      let data1: Catalog = new Catalog();
        data1.ID = "1";
        data1.Name = "Catalog 1";
        data1.CreationDate = new Date();
        data1.Size = "15Mb";
    
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
        data.push(data1);
    
        let data2: Catalog = new Catalog();
        data2.ID = "1";
        data2.Name = "Catalog 2";
        data2.CreationDate = new Date();
        data2.Size = "20Mb";
    
        data.push(data2);
    }
    else if (parent.ID == "1"){
      let data3: Catalog = new Catalog();
      data3.ID = "3";
      data3.Name = "Catalog 3";
      data3.CreationDate = new Date();
      data3.Size = "200Mb";
    
      data.push(data3);
    }
    else {
      let data4: Catalog = new Catalog();
      data4.ID = "4";
      data4.Name = "Catalog 4";
      data4.CreationDate = new Date();
      data4.Size = "256Gb";
    
      data.push(data4);
    }

    return data;
  }
}
