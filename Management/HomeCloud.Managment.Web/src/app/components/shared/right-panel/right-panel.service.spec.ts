import { TestBed, inject } from '@angular/core/testing';

import { RightPanelService } from './right-panel.service';

describe('RightPanelService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RightPanelService]
    });
  });

  it('should be created', inject([RightPanelService], (service: RightPanelService) => {
    expect(service).toBeTruthy();
  }));
});
