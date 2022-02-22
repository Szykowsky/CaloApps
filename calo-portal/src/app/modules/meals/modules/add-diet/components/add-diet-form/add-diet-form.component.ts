import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'calo-add-diet-form',
  templateUrl: './add-diet-form.component.html',
  styleUrls: ['./add-diet-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddDietFormComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
