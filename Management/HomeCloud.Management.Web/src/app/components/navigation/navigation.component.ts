import { Component, OnInit, OnDestroy } from '@angular/core';
import { ISubscription, Subscription } from 'rxjs/Subscription';

import { PagedArray } from '../../models/paged-array';
import { Storage } from '../../models/storage';

import { StorageService } from '../../services/storage/storage.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit, OnDestroy {

  private storages: PagedArray<Storage> = new PagedArray<Storage>();
  
  private listSubscription: Subscription = null;
  private getSubscription: Subscription = null;

  constructor(
    private storageService: StorageService) {
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
  }

  public canSelectStorage(storage: Storage): boolean {
    return storage.hasGet && storage.hasGet();
  }

  public selectStorage(storage: Storage) {
    if (this.canSelectStorage(storage)) {
      let index: number = this.storages.indexOf(storage);

      this.getSubscription = this.storageService.get(storage).subscribe(item => {
        this.storageService.selectCatalog(item);
      });
    }
  }
}
