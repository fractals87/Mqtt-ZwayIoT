import { TestBed } from '@angular/core/testing';

import { DeviceiotService } from './deviceiot.service';

describe('DeviceiotService', () => {
  let service: DeviceiotService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeviceiotService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
