import { createAction, props } from '@ngrx/store';
import { Datetype } from '../../models/meals-filter-model';
import { MealsQueryResult } from '../../models/meals-model';

export const enum MealsActionTypes {
    FetchMeals = '[MEALS] Fetch init',
    FetchMealsSuccess = '[MEALS] Fetch success',
    FetchMealsFail = '[MEALS] Fetch fail',
    FetchDiets = '[MEALS] Fetch diets',
    FetchDietsSuccess = '[MEALS] Fetch diets success',
    FetchDietsFail = '[MEALS] Fetch diets fail',
}

export const fetchMeals = createAction(
    MealsActionTypes.FetchMeals,
    props<{
        dietId: string;
        dateType: Datetype;
        dayNumer: number | null;
        monthNumber: number | null;
    }>()
);

export const fetchMealsSuccess = createAction(
    MealsActionTypes.FetchMealsSuccess,
    props<{ meals: MealsQueryResult }>()
);

export const fetchMealsFail = createAction(
    MealsActionTypes.FetchMealsFail,
    props<{ error: {} }>()
);

export const fetchDiets = createAction(
    MealsActionTypes.FetchDiets,
    props<{
        userId: string;
    }>()
);

export const fetchDietsSuccess = createAction(
    MealsActionTypes.FetchDietsSuccess,
    props<{ diets: { key: string; value: string } }>()
);

export const fetchDietsFail = createAction(
    MealsActionTypes.FetchDietsFail,
    props<{ error: {} }>()
);
