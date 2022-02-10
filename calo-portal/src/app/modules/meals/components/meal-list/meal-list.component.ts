import {
    ChangeDetectionStrategy,
    Component,
    Input,
    OnInit,
} from '@angular/core';
import { Meal } from '../../models/meals-model';

@Component({
    selector: 'calo-meal-list',
    templateUrl: './meal-list.component.html',
    styleUrls: ['./meal-list.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MealListComponent implements OnInit {
    @Input() meals: Meal[] | null = [];

    constructor() {}

    ngOnInit(): void {}
}
