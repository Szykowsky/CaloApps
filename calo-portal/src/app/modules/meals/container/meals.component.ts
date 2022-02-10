import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MealsFacade } from '../meals.facade';
import { Meal } from '../models/meals-model';

@Component({
    selector: 'calo-meals',
    templateUrl: './meals.component.html',
    styleUrls: ['./meals.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MealsComponent implements OnInit {
    readonly meals$: Observable<Meal[]> = this.mealsFacade.meals$;

    constructor(private mealsFacade: MealsFacade) {}

    ngOnInit(): void {}
}
