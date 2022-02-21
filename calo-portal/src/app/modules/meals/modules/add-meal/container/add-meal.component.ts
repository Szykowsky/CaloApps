import {
    ChangeDetectionStrategy,
    Component,
    OnDestroy,
    OnInit,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import {
    combineLatest,
    map,
    Observable,
    pipe,
    Subscription,
    take,
    tap,
} from 'rxjs';
import { MealsFacade } from '../../../meals.facade';
import { NoDietDialogComponent } from '../components/no-diet-dialog/no-diet-dialog.component';
import { AddMealModel } from '../models/add-meal-model';

@Component({
    selector: 'calo-add-meal',
    templateUrl: './add-meal.component.html',
    styleUrls: ['./add-meal.component.scss'],
    // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddMealComponent implements OnInit, OnDestroy {
    diets$: Observable<{ [key: string]: string } | null> =
        this.mealsFacade.diets$;
    isLoading$: Observable<boolean> = this.mealsFacade.isLoading$;

    addMealForm: FormGroup | undefined;
    dietsSubscription: Subscription | undefined;

    constructor(
        private mealsFacade: MealsFacade,
        private fb: FormBuilder,
        private dialog: MatDialog,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.mealsFacade.getDiets('9cd5fbf8-a407-4491-db67-08d9f5621f10');

        this.dietsSubscription = combineLatest([this.diets$, this.isLoading$])
            .pipe(
                tap(([diets, isLoading]) => {
                    if (isLoading) {
                        return;
                    }

                    let [first] = Object.keys(diets || []);
                    console.log('diets', first);

                    !diets
                        ? this.showNoDietsModal()
                        : (this.addMealForm = this.fb.group({
                              diet: [
                                  Object.keys(diets).length === 1
                                      ? first
                                      : null,
                                  [Validators.required],
                              ],
                              name: [null, [Validators.required]],
                              kcal: [
                                  null,
                                  [Validators.required, Validators.min(0)],
                              ],
                              date: [new Date(), [Validators.required]],
                          }));
                })
            )
            .subscribe();
    }

    ngOnDestroy(): void {
        this.dietsSubscription?.unsubscribe();
    }

    submitForm($event: AddMealModel) {
        console.log($event);
        this.mealsFacade.addMeal($event);
    }

    showNoDietsModal() {
        const dialogRef = this.dialog.open(NoDietDialogComponent);

        dialogRef.afterClosed().subscribe((result) => {
            result
                ? this.router.navigateByUrl('/meals')
                : this.router.navigateByUrl('/'); // TODO dodoać opcję dodawania diet
        });
    }
}
