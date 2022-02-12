import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MealsComponent } from './container/meals/meals.component';
import { MealListComponent } from './components/meal-list/meal-list.component';
import { MealApiService } from './api/meal-api.service';
import { MealsFacade } from './meals.facade';
import { MealsRoutingModule } from './meals.routing-module';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatGridListModule } from '@angular/material/grid-list';

@NgModule({
    declarations: [MealsComponent, MealListComponent],
    imports: [
        CommonModule,
        MealsRoutingModule,
        MatCardModule,
        MatIconModule,
        MatButtonModule,
        MatGridListModule
    ],
    providers: [MealApiService, MealsFacade],
})
export class MealsModule {}
