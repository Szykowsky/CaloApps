import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { map, Observable, pipe, take } from 'rxjs';
import { MealsFacade } from '../../../meals.facade';

@Component({
    selector: 'calo-add-meal',
    templateUrl: './add-meal.component.html',
    styleUrls: ['./add-meal.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddMealComponent implements OnInit {
    readonly diets$: Observable<{ key: string; value: string }[] | null> =
        this.mealsFacade.diets$;

    constructor(private mealsFacade: MealsFacade) {}

    ngOnInit(): void {
        this.mealsFacade.getDiets('68D9B10A-5608-4E44-68A5-08D9EE630B5E');

        this.diets$.pipe(
            take(1),
            map((value) => (!value || value.length === 0 ? null : ''))
        );
    }
}