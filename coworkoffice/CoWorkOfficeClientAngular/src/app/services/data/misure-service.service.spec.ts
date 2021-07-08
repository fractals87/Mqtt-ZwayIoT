import { TestBed } from '@angular/core/testing';

import { MisureServiceService } from './misure-service.service';

describe('MisureServiceService', () => {
  let service: MisureServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MisureServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
