import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { PagedArray } from '../../../models/paged-array';
import { StorageData } from '../../../models/storage-data';
import { Catalog } from '../../../models/catalog';
import { CatalogState } from '../../../models/catalog-state';
import { CatalogStateChanged } from '../../../models/catalog-state-changed';

import { CatalogService } from '../../../services/catalog/catalog.service';
import { ProgressService } from '../../../services/shared/progress/progress.service';

@Component({
  selector: 'app-catalog-list',
  templateUrl: './catalog-list.component.html',
  styleUrls: ['./catalog-list.component.css']
})
export class CatalogListComponent implements OnInit, OnDestroy {

  private data: PagedArray<StorageData> = new PagedArray<StorageData>();

  private catalogChangedSubscription: ISubscription = null;
  private listSubscription: ISubscription = null;

  constructor(
    private progressService: ProgressService,
    private catalogService: CatalogService) {
      this.catalogChangedSubscription = this.catalogService.catalogChanged$.subscribe(catalog => {        
        this.data.splice(0);
        this.open(catalog);
      });
    }

  ngOnInit() {
  }

  private select(catalog: Catalog) {
    this.catalogService.onCatalogChanged(catalog);
  }

  private open(catalog: Catalog) {
    this.progressService.show();

    this.listSubscription = this.catalogService.list(catalog).subscribe(data => {
      this.data = data;

      this.progressService.hide();
    }, error => {
      this.progressService.hide();
    });
  }

  private get canCreateCatalog(): boolean {
    return this.catalogService.hasCreateCatalog();
  }

  private createCatalog() {
    if (this.canCreateCatalog) {
      
    }
  }

  private remove(catalog: Catalog) {    
    let item: StorageData = this.data.find(item => item.isCatalog && item.id == catalog.id);

    let index: number = this.data.indexOf(item);
    if (index >= 0) {
      this.data.splice(index, 1);
    }
  }

  private save(catalog: Catalog) {    
    // let item: StorageData = this.data.find(item => item.isCatalog && item.id == catalog.id);

    // let index: number = this.data.indexOf(item);
    // if (index >= 0) {
    //   this.data[index] = catalog;
    // }
  }

  ngOnDestroy(): void {

    if (this.listSubscription) {
      this.listSubscription.unsubscribe();
      this.listSubscription = null;
    }

    this.data.splice(0);
    this.data = null;
  }
}
