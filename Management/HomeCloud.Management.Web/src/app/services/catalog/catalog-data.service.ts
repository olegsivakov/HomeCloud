import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Catalog } from '../../models/catalog';
import { ResourceService } from '../resource/resource.service';
import { Subject } from 'rxjs/Subject';
import { Observable } from 'rxjs/Observable';
import { StorageData } from '../../models/storage-data';
import { CatalogStateChanged } from '../../models/catalog-state-changed';

const url: string = "http://localhost/catalogs/";

@Injectable()
export class CatalogDataService extends HttpService<Catalog> {

  private stateChangedSource: Subject<CatalogStateChanged> = new Subject<CatalogStateChanged>();

  stateChanged$ = this.stateChangedSource.asObservable();

  constructor(protected resourceService: ResourceService) {    
    super(resourceService, url);
  }

  public load(catalog: Catalog): Observable<Array<StorageData>> {
    return Observable.create(observer => {
      observer.next(this.Initialize(catalog));
    });
  }

  public onStateChanged(args: CatalogStateChanged): void {
    this.stateChangedSource.next(args);
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
