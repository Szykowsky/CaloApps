import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MealsComponent } from './container/meals/meals.component';

const routes: Routes = [
    {
        path: '', component: MealsComponent,
    },
    {
        path: 'add',
        children: [
            {
                path: '',
                loadChildren: () =>
                    import('./modules/add-meal/add-meal.module').then((m) => m.AddMealModule),
            },
            {
                path: 'diet',
                loadChildren: () =>
                    import('./modules/add-diet/add-diet.module').then((m) => m.AddDietModule),
            }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MealsRoutingModule { }
