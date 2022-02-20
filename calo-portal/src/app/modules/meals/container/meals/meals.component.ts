import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { MealsFacade } from '../../meals.facade';
import { Datetype } from '../../models/meals-filter-model';
import { MealsQueryResult } from '../../models/meals-model';

@Component({
    selector: 'calo-meals',
    templateUrl: './meals.component.html',
    styleUrls: ['./meals.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MealsComponent implements OnInit {
    readonly meals$: Observable<MealsQueryResult> = this.mealsFacade.meals$;

    constructor(private mealsFacade: MealsFacade) { }

    ngOnInit(): void {
        this.mealsFacade.getMealList({
            dietId: '68D9B10A-5608-4E44-68A5-08D9EE630B5E',
            dateType: Datetype.Month,
            dayNumer: 2,
            monthNumber: 2,
        });
    }
}
