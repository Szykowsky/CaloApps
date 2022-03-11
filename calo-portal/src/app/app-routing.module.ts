import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
    },
    {
        path: 'meals',
        loadChildren: () =>
            import('./modules/meals/meals.module').then((m) => m.MealsModule),
    },
    {
        path: 'dashboard',
        loadChildren: () =>
            import('./modules/dashboard/dashboard.module').then((m) => m.DashboardModule),
    },
    {
        path: '**',
        component: PageNotFoundComponent,
        pathMatch: 'full'
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
