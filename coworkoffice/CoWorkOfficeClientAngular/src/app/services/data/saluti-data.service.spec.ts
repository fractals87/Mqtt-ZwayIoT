import { TestBed } from '@angular/core/testing';

import { SalutiDataService } from './saluti-data.service';

describe('SalutiDataService', () => {
  let service: SalutiDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SalutiDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
