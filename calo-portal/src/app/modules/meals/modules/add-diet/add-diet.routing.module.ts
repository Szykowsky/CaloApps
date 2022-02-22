import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddDietComponent } from './container/add-diet.component';

const routes: Routes = [{ path: '', component: AddDietComponent }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AddDietRoutingModule {}
