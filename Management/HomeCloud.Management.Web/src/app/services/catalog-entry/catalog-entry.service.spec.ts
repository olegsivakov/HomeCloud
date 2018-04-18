import { TestBed, inject } from '@angular/core/testing';

import { CatalogEntryService } from './catalog-entry.service';

describe('CatalogEntryService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CatalogEntryService]
    });
  });

  it('should be created', inject([CatalogEntryService], (service: CatalogEntryService) => {
    expect(service).toBeTruthy();
  }));
});
