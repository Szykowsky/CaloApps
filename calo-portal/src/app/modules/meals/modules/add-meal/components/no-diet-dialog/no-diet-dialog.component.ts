import {
    ChangeDetectionStrategy,
    Component,
    Inject,
    OnInit,
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'calo-no-diet-dialog',
    templateUrl: './no-diet-dialog.component.html',
    styleUrls: ['./no-diet-dialog.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NoDietDialogComponent implements OnInit {
    constructor(public dialogRef: MatDialogRef<NoDietDialogComponent>) {}

    ngOnInit(): void {}
}
