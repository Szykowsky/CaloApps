import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'calo-add-meal-form',
  templateUrl: './add-meal-form.component.html',
  styleUrls: ['./add-meal-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddMealFormComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
