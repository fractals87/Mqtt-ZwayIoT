import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageprenotazioneComponent } from './manageprenotazione.component';

describe('ManageprenotazioneComponent', () => {
  let component: ManageprenotazioneComponent;
  let fixture: ComponentFixture<ManageprenotazioneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageprenotazioneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageprenotazioneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
