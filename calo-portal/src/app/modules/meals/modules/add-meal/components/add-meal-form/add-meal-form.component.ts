import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import {  FormGroup,  } from '@angular/forms';

@Component({
  selector: 'calo-add-meal-form',
  templateUrl: './add-meal-form.component.html',
  styleUrls: ['./add-meal-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddMealFormComponent {
  @Input() addMealForm: FormGroup | undefined;
  @Output() submitForm: EventEmitter<FormGroup> = new EventEmitter();

  addMeal() {
    this.submitForm.emit(this.addMealForm?.value);
  }

}
