import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription } from 'rxjs/Subscription';

import { PagedArray } from '../../models/paged-array';
import { Storage } from '../../models/storage';

import { StorageService } from '../../services/storage/storage.service';
import { CatalogService } from '../../services/catalog/catalog.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit, OnDestroy {

  private storages: PagedArray<Storage> = new PagedArray<Storage>();

  private listSubscription: ISubscription = null;
  private selfSubscription: ISubscription = null;
  private catalogSubscription: ISubscription = null;

  constructor(
    private storageService: StorageService,
    private catalogService: CatalogService) {
      this.listSubscription = this.storageService.list(20).subscribe(data => {
        this.storages = data;
      });
  }

  ngOnInit() {
    
  }

  ngOnDestroy(): void {
    if (this.listSubscription) {
      this.listSubscription.unsubscribe();
      this.listSubscription = null;
    }

    if (this.catalogSubscription) {
      this.catalogSubscription.unsubscribe();
      this.catalogSubscription = null;
    }

    if (this.selfSubscription) {
      this.selfSubscription.unsubscribe();
      this.selfSubscription = null;
    }
  }

  public canSelectStorage(storage: Storage): boolean {
    return storage.hasSelf();
  }

  public selectStorage(storage: Storage) {
    if (this.canSelectStorage(storage)) {
      let index: number = this.storages.indexOf(storage);

      this.selfSubscription = this.storageService.self(storage).subscribe(item => {
        if (item.hasCatalog()) {
          this.catalogSubscription = this.storageService.catalog(item).subscribe(catalog => {
            this.catalogService.onCatalogChanged(catalog);
          });
        }
      });     
    }
  }
}
