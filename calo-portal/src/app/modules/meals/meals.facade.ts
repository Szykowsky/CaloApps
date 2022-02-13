import { Injectable } from '@angular/core';
import { Action, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { MealsQueryResult } from './models/meals-model';
import { MealState } from './store/reducers/meals.reducer';
import { Datetype } from './models/meals-filter-model';
import { fromMealsSelectors } from './store';
import * as MealActions from './store/actions/meals.action';

@Injectable()
export class MealsFacade {
    readonly meals$: Observable<MealsQueryResult> = this.store.select(
        fromMealsSelectors.selectMeals
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

    private dispatch(action: Action): void {
        this.store.dispatch(action);
    }
}
