import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MealsComponent } from './container/meals/meals.component';

const routes: Routes = [
    {
        path: '', component: MealsComponent,
    },
    {
        path: 'add/:dietId',
        loadChildren: () =>
            import('./modules/add-meal/add-meal.module').then((m) => m.AddMealModule),
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MealsRoutingModule { }
