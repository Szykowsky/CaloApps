import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Meal } from '../models/meals-model';

@Injectable()
export class MealApiService {
    readonly API = 'https://localhost:7095/api/meal';

    constructor(private http: HttpClient) {}

    getMeals(userId: string): Observable<Meal[]> {
        return this.http.get<Meal[]>(`${this.API}/${userId}`);
    }
}
