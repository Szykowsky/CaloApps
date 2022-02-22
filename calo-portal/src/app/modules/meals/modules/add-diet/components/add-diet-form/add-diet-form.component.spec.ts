import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddDietFormComponent } from './add-diet-form.component';

describe('AddDietFormComponent', () => {
  let component: AddDietFormComponent;
  let fixture: ComponentFixture<AddDietFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddDietFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddDietFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
