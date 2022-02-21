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
            dietId: '5c56baaf-7144-41db-338a-08d9f562247d',
            dateType: Datetype.Month,
            dayNumer: 2,
            monthNumber: 2,
        });
    }
}
