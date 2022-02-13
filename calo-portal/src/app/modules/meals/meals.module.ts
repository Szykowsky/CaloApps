import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MealsComponent } from './container/meals/meals.component';
import { MealListComponent } from './components/meal-list/meal-list.component';
import { MealApiService } from './api/meal-api.service';
import { MealsFacade } from './meals.facade';
import { MealsRoutingModule } from './meals.routing-module';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { StoreModule } from '@ngrx/store';
import { fromMeals, MealsEffects } from './store';
import { EffectsModule } from '@ngrx/effects';

@NgModule({
    declarations: [MealsComponent, MealListComponent],
    imports: [
        CommonModule,
        MealsRoutingModule,
        MaterialModule,
        StoreModule.forFeature(fromMeals.mealsFeatureKey, fromMeals.reducer),
        EffectsModule.forFeature([MealsEffects])
    ],
    providers: [MealApiService, MealsFacade],
})
export class MealsModule {}
