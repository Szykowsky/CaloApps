import { createFeatureSelector, createSelector } from "@ngrx/store";
import * as fromMeals from '../reducers/meals.reducer';

export const selectMealsFeatureState = createFeatureSelector<fromMeals.MealState>(fromMeals.mealsFeatureKey);

export const selectMeals = createSelector(
    selectMealsFeatureState,
    (state: fromMeals.MealState) => state.meals
);

export const selectDiets = createSelector(
    selectMealsFeatureState,
    (state: fromMeals.MealState) => state.diets
);

