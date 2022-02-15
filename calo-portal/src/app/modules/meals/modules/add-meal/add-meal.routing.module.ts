import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddMealComponent } from './container/add-meal.component';

const routes: Routes = [{ path: '', component: AddMealComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AddMealRoutingModule {}
