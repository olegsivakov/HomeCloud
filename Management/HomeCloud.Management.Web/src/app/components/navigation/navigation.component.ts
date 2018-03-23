import { Component, OnInit } from '@angular/core';
import { StorageService } from '../../services/storage/storage.service';
import { PagedArray } from '../../models/paged-array';
import { StorageStateService } from '../../services/storage-state/storage-state.service';
import { Storage } from '../../models/storage';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  private storages: PagedArray<Storage> = new PagedArray<Storage>();

  constructor(
    private storageService: StorageService,
    private storageStateService: StorageStateService) {
  }

  ngOnInit() {
    this.storageService.list(20).subscribe(data => {
      Object.assign(this.storages, data);
    });
  }

  public selectStorage(storage: Storage) {
    this.storageStateService.selectStorage(storage);
  }
}
