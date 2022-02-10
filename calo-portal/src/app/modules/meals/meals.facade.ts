import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MealApiService } from './api/meal-api.service';
import { Meal } from './models/meals-model';

@Injectable()
export class MealsFacade {
    readonly meals$: Observable<Meal[]> = this.getMealList();

    constructor(private mealApi: MealApiService) {}

    //TODO get it from store ngrx
    getMealList(): Observable<Meal[]> {
        return this.mealApi.getMeals('3FA85F64-5717-4562-B3FC-2C963F66AFA6');
    }
}
