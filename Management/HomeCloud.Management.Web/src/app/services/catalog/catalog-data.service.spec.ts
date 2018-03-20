import { TestBed, inject } from '@angular/core/testing';

import { CatalogDataService } from './catalog-data.service';

describe('CatalogDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CatalogDataService]
    });
  });

  it('should be created', inject([CatalogDataService], (service: CatalogDataService) => {
    expect(service).toBeTruthy();
  }));
});
