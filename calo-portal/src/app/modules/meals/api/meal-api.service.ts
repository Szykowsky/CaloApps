import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Datetype } from '../models/meals-filter-model';
import { Meal, MealsQueryResult } from '../models/meals-model';

@Injectable()
export class MealApiService {
    readonly API = 'https://localhost:7095/api/meal';

    constructor(private http: HttpClient) {}

    getMeals(
        dietId: string,
        dateType: Datetype,
        dayNumber: number | null,
        monthNumber: number | null
    ): Observable<MealsQueryResult> {
        const dayNumberQuery = dayNumber ? `dayNumber=${dayNumber}` : ``;
        const monthNumberQuery = monthNumber
            ? `monthNumber=${monthNumber}`
            : ``;
        const query = `dateType=${dateType}&${dayNumberQuery}&${monthNumberQuery}`;

        return this.http.get<MealsQueryResult>(`${this.API}/${dietId}?${query}`);
    }
}
