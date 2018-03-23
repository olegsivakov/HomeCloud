import { TestBed, inject } from '@angular/core/testing';

import { StorageStateService } from './storage-state.service';

describe('StorageStateService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [StorageStateService]
    });
  });

  it('should be created', inject([StorageStateService], (service: StorageStateService) => {
    expect(service).toBeTruthy();
  }));
});
