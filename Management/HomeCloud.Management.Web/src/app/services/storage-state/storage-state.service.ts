import { Injectable } from '@angular/core';
import { Storage } from '../../models/storage';

@Injectable()
export class StorageStateService {

  public storage: Storage = null;

  constructor() { }

  public selectStorage(storage: Storage): void {
    this.storage = storage;
  }
}
