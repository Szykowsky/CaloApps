import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'calo-add-diet',
  templateUrl: './add-diet.component.html',
  styleUrls: ['./add-diet.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AddDietComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
