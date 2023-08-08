import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FuelLogDetailsComponent } from './fuel-log-details.component';

describe('FuelLogDetailsComponent', () => {
  let component: FuelLogDetailsComponent;
  let fixture: ComponentFixture<FuelLogDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FuelLogDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FuelLogDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
