import { Action, createReducer, on } from '@ngrx/store';
import { Meal, MealsQueryResult } from '../../models/meals-model';
import * as MealsActions from '../actions/meals.action';

export const mealsFeatureKey = 'MealsFeatureKey';

export interface MealState {
    isLoading: boolean;
    isError: boolean;
    meals: MealsQueryResult;
    diets: { [key: string]: string } | null;
    error: any;
}

const initialState: MealState = {
    isLoading: false,
    isError: false,
    meals: {
        paginationBase: null,
        queryResult: [],
    },
    diets: null,
    error: null,
};

const mealsReducer = createReducer(
    initialState,
    on(MealsActions.fetchMeals, (state) => ({ ...state, isLoading: true })),
    on(MealsActions.fetchMealsSuccess, (state, { meals }) => ({
        ...state,
        isLoading: false,
        meals,
    })),
    on(MealsActions.fetchMealsFail, (state, { error }) => ({
        ...state,
        isLoading: false,
        isError: true,
    })),
    on(MealsActions.fetchDiets, (state) => ({ ...state, isLoading: true })),
    on(MealsActions.fetchDietsSuccess, (state, { diets }) => ({
        ...state,
        isLoading: false,
        diets,
    })),
    on(MealsActions.fetchDietsFail, (state, { error }) => ({
        ...state,
        isLoading: false,
        isError: true,
    })),
    on(MealsActions.addMeals, (state) => ({ ...state, isLoading: true })),
    on(MealsActions.addMealsSuccess, (state) => ({ ...state, isLoading: false })),
    on(MealsActions.addMealsFail, (state, { error }) => ({
        ...state,
        isLoading: false,
        isError: true,
    })),
);

export function reducer(state: MealState, action: Action) {
    return mealsReducer(state, action);
}
