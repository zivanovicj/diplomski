import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFuellogComponent } from './add-fuellog.component';

describe('AddFuellogComponent', () => {
  let component: AddFuellogComponent;
  let fixture: ComponentFixture<AddFuellogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddFuellogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddFuellogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
