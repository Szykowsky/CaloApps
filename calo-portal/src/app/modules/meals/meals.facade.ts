import { Injectable } from '@angular/core';
import { Action, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { MealsQueryResult } from './models/meals-model';
import { MealState } from './store/reducers/meals.reducer';
import { Datetype } from './models/meals-filter-model';
import { fromMealsSelectors, MealsActions } from './store';
import * as MealActions from './store/actions/meals.action';
import { AddMealModel } from './modules/add-meal/models/add-meal-model';

@Injectable()
export class MealsFacade {
    readonly meals$: Observable<MealsQueryResult> = this.store.select(
        fromMealsSelectors.selectMeals
    );
    readonly diets$: Observable<{ [key: string]: string } | null> =
        this.store.select(fromMealsSelectors.selectDiets);

    readonly isLoading$: Observable<boolean> = this.store.select(
        fromMealsSelectors.selectIsLoading
    );

    constructor(private store: Store<MealState>) {}

    getMealList(payload: {
        dietId: string;
        dateType: Datetype;
        dayNumer: number | null;
        monthNumber: number | null;
    }) {
        const { dietId, dateType, dayNumer, monthNumber } = payload;

        this.dispatch(
            MealActions.fetchMeals({ dietId, dateType, dayNumer, monthNumber })
        );
    }

    getDiets(userId: string) {
        this.dispatch(MealActions.fetchDiets({ userId }));
    }

    addMeal(addMealModel: AddMealModel) {
        this.dispatch(MealsActions.addMeals({addMealModel}))
    }

    private dispatch(action: Action): void {
        this.store.dispatch(action);
    }
}
