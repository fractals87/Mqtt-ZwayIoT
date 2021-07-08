import { TestBed } from '@angular/core/testing';

import { AuthInterceptService } from './auth-intercept.service';

describe('AuthInterceptService', () => {
  let service: AuthInterceptService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthInterceptService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
