import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMealFormComponent } from './add-meal-form.component';

describe('AddMealFormComponent', () => {
  let component: AddMealFormComponent;
  let fixture: ComponentFixture<AddMealFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddMealFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMealFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
