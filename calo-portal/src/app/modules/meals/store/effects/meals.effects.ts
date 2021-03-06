import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, switchMap } from 'rxjs';
import { MealApiService } from '../../api/meal-api.service';
import { MealsActionTypes } from '../actions/meals.action';
import * as MealsActions from '../actions/meals.action';
import { MealsQueryResult } from '../../models/meals-model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class MealsEffects {
    loadMeals$ = createEffect(() =>
        this.actions$.pipe(
            ofType(MealsActionTypes.FetchMeals),
            switchMap(({ dietId, dateType, dayNumber, monthNumber }) =>
                this.mealsApiService
                    .getMeals(dietId, dateType, dayNumber, monthNumber)
                    .pipe(
                        map((meals: MealsQueryResult) =>
                            MealsActions.fetchMealsSuccess({ meals })
                        )
                    )
            )
        )
    );

    loadDiets$ = createEffect(() =>
        this.actions$.pipe(
            ofType(MealsActionTypes.FetchDiets),
            switchMap(({ userId }) =>
                this.mealsApiService
                    .getDiets(userId)
                    .pipe(
                        map((diets: { [key: string]: string }) =>
                            MealsActions.fetchDietsSuccess({ diets })
                        )
                    )
            )
        )
    );

    addMeal$ = createEffect(() =>
        this.actions$.pipe(
            ofType(MealsActionTypes.AddMeals),
            switchMap(({ addMealModel }) =>
                this.mealsApiService.addMeals(addMealModel).pipe(
                    map((value: any) => {
                        this.snackBar.open(value.message, 'Close');
                        return MealsActions.addMealsSuccess();
                    })
                )
            )
        )
    );

    constructor(
        private actions$: Actions,
        private mealsApiService: MealApiService,
        private snackBar: MatSnackBar
    ) {}
}
