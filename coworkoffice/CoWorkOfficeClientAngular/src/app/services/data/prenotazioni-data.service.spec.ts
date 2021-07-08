import { TestBed } from '@angular/core/testing';

import { PrenotazioniDataService } from './prenotazioni-data.service';

describe('PrenotazioniDataService', () => {
  let service: PrenotazioniDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PrenotazioniDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
