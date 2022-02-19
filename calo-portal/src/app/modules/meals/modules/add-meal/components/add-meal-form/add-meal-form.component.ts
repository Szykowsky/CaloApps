import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'calo-add-meal-form',
  templateUrl: './add-meal-form.component.html',
  styleUrls: ['./add-meal-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddMealFormComponent {
  addMealForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.addMealForm = this.fb.group(
      {
        diet: [null, [Validators.required]],
        name: [null, [Validators.required]],
        kcal: [null, [Validators.required]],
        date: [Date.now, [Validators.required]]
      })
   }

  addMeal() {
    console.log(this.addMealForm.value, 'addMeal()');
  }

}
