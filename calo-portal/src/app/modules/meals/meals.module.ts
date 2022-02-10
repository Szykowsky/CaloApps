import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MealsComponent } from './container/meals.component';
import { MealListComponent } from './components/meal-list/meal-list.component';
import { MealApiService } from './api/meal-api.service';
import { MealsFacade } from './meals.facade';
import { MealsRoutingModule } from './meals.routing-module';

@NgModule({
    declarations: [MealsComponent, MealListComponent],
    imports: [CommonModule, MealsRoutingModule],
    providers: [MealApiService, MealsFacade],
})
export class MealsModule {}
