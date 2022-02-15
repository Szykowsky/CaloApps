import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddMealComponent } from './container/add-meal.component';
import { AddMealFormComponent } from './components/add-meal-form/add-meal-form.component';
import { AddMealRoutingModule } from './add-meal.routing.module';
import { MaterialModule } from 'src/app/shared/modules/material.module';

@NgModule({
  declarations: [
    AddMealComponent,
    AddMealFormComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    AddMealRoutingModule
  ]
})
export class AddMealModule { }
