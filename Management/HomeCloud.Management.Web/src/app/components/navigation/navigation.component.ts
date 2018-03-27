import { Component, OnInit } from '@angular/core';
import { StorageService } from '../../services/storage/storage.service';
import { PagedArray } from '../../models/paged-array';
import { Storage } from '../../models/storage';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  private storages: PagedArray<Storage> = new PagedArray<Storage>();

  constructor(private storageService: StorageService) {
  }

  ngOnInit() {
    this.storageService.list(20).subscribe(data => {
      this.storages = data;
    });
  }

  public selectStorage(storage: Storage) {
    let index: number = this.storages.indexOf(storage);
    this.storageService.item(index).subscribe(item => {
      this.storageService.select(item);
    });
  }
}
