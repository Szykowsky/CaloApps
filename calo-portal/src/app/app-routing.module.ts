import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'meals',
        pathMatch: 'full'
    },
    {
        path: 'meals',
        loadChildren: () =>
            import('./modules/meals/meals.module').then((m) => m.MealsModule),
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
