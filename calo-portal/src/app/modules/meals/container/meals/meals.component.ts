import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { MealsFacade } from '../../meals.facade';
import { Meal } from '../../models/meals-model';

@Component({
    selector: 'calo-meals',
    templateUrl: './meals.component.html',
    styleUrls: ['./meals.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MealsComponent implements OnInit {
    //readonly meals$: Observable<Meal[]> = this.mealsFacade.meals$;
    meal: Meal = {id: 'qweqwe',
    date: new Date(),
    name: 'qweqwe',
    kcal: 1000}

    readonly meals$: Observable<Meal[]> = of([this.meal, this.meal, this.meal, this.meal, this.meal, this.meal, this.meal]);

    constructor(private mealsFacade: MealsFacade) {}

    ngOnInit(): void {}
}
