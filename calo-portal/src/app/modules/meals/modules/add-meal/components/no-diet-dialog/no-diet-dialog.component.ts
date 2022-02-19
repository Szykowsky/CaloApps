import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'calo-no-diet-dialog',
  templateUrl: './no-diet-dialog.component.html',
  styleUrls: ['./no-diet-dialog.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NoDietDialogComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
