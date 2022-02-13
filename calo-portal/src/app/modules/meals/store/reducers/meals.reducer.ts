import { Action, createReducer, on } from '@ngrx/store';
import { Meal, MealsQueryResult } from '../../models/meals-model';
import * as MealsActions from '../actions/meals.action';

export const mealsFeatureKey = 'MealsFeatureKey';

export interface MealState {
    isLoading: boolean;
    isError: boolean;
    meals: MealsQueryResult;
    error: any;
}

const initialState: MealState = {
    isLoading: false,
    isError: false,
    meals: {
        paginationBase: null,
        queryResult: []
    },
    error: null,
};

const mealsReducer = createReducer(
    initialState,
    on(MealsActions.fetchMeals, (state) => ({ ...state, isLoading: true })),
    on(MealsActions.fetchMealsSuccess, (state, { meals }) => ({ ...state, meals })),
    on(MealsActions.fetchMealsFail, (state, { error }) => ({ ...state, isError: true }))
);

export function reducer(state: MealState, action: Action) {
    return mealsReducer(state, action);
}