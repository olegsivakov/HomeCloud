import { TestBed, inject } from '@angular/core/testing';

import { CatalogStateService } from './catalog-state.service';

describe('CatalogStateService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CatalogStateService]
    });
  });

  it('should be created', inject([CatalogStateService], (service: CatalogStateService) => {
    expect(service).toBeTruthy();
  }));
});
