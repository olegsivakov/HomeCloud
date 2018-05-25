import { TestBed, inject } from '@angular/core/testing';

import { ClientApplicationService } from './client-application.service';

describe('ClientApplicationService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClientApplicationService]
    });
  });

  it('should be created', inject([ClientApplicationService], (service: ClientApplicationService) => {
    expect(service).toBeTruthy();
  }));
});
