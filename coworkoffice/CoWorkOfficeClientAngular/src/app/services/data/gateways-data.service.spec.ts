import { TestBed } from '@angular/core/testing';

import { GatewaysDataService } from './gateways-data.service';

describe('GatewaysDataService', () => {
  let service: GatewaysDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GatewaysDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
