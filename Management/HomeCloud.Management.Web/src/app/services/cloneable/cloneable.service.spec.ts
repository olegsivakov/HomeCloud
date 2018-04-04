import { TestBed, inject } from '@angular/core/testing';

import { CloneableService } from './cloneable.service';

describe('CloneableService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CloneableService]
    });
  });

  it('should be created', inject([CloneableService], (service: CloneableService) => {
    expect(service).toBeTruthy();
  }));
});
