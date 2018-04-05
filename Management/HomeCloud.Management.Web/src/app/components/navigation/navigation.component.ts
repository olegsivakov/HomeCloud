import { Component, OnInit, OnDestroy } from '@angular/core';
import { StorageService } from '../../services/storage/storage.service';
import { PagedArray } from '../../models/paged-array';
import { Storage } from '../../models/storage';
import { CatalogStateChanged } from '../../models/catalog-state-changed';
import { Catalog } from '../../models/catalog';
import { CatalogService } from '../../services/catalog/catalog.service';
import { CatalogState } from '../../models/catalog-state';
import { ISubscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit, OnDestroy {

  private storages: PagedArray<Storage> = new PagedArray<Storage>();

  private listSubscription: ISubscription = null;
  private getSubscription: ISubscription = null;
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

    if (this.getSubscription) {
      this.getSubscription.unsubscribe();
      this.getSubscription = null;
    }

    if (this.catalogSubscription) {
      this.catalogSubscription.unsubscribe();
      this.catalogSubscription = null;
    }
  }

  public selectStorage(storage: Storage) {
    let index: number = this.storages.indexOf(storage);

    this.getSubscription = this.storageService.item(index).subscribe(item => {
      this.catalogSubscription = this.storageService.catalog(item).subscribe(catalog => {
        let args: CatalogStateChanged = new CatalogStateChanged(catalog, CatalogState.open);
        this.catalogService.onStateChanged(args);
      });
    });
  }
}
