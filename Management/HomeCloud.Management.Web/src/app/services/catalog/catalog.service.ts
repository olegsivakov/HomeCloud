import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';

import { RelationArray } from '../../models/http/relation-array';
import { PagedArray } from '../../models/paged-array';
import { Catalog } from '../../models/catalog';
import { StorageData } from '../../models/storage-data';
import { CatalogStateChanged } from '../../models/catalog-state-changed';

import { HttpService } from '../http/http.service';

const url: string = "http://localhost/catalogs/";

@Injectable()
export class CatalogService extends HttpService<RelationArray, Catalog> {

  private stateChangedSource: Subject<CatalogStateChanged> = new Subject<CatalogStateChanged>();

  stateChanged$ = this.stateChangedSource.asObservable();

  constructor(protected httpClient: HttpClient) {    
    super(httpClient, url);
  }

  public load(catalog: Catalog): Observable<PagedArray<StorageData>> {
    return Observable.create(observer => {
      let data = this.Initialize(catalog);

      observer.next(data);
      observer.complete();
    });
  }

  public onStateChanged(args: CatalogStateChanged): void {
    this.stateChangedSource.next(args);
  }

  private Initialize(parent: Catalog): PagedArray<StorageData> {
    let data: PagedArray<StorageData> = new PagedArray<StorageData>();

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
