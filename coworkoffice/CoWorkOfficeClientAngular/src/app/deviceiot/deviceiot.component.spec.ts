import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceiotComponent } from './deviceiot.component';

describe('DeviceiotComponent', () => {
  let component: DeviceiotComponent;
  let fixture: ComponentFixture<DeviceiotComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeviceiotComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeviceiotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
