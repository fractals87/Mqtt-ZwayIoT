import { TestBed } from '@angular/core/testing';

import { AuthappService } from './authapp.service';

describe('AuthappService', () => {
  let service: AuthappService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthappService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
