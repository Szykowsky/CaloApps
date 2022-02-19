import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoDietDialogComponent } from './no-diet-dialog.component';

describe('NoDietDialogComponent', () => {
  let component: NoDietDialogComponent;
  let fixture: ComponentFixture<NoDietDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NoDietDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NoDietDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
