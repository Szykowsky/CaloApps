import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddMealComponent } from './container/add-meal.component';
import { AddMealFormComponent } from './components/add-meal-form/add-meal-form.component';
import { AddMealRoutingModule } from './add-meal.routing.module';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { NoDietDialogComponent } from './components/no-diet-dialog/no-diet-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AddMealComponent,
    AddMealFormComponent,
    NoDietDialogComponent,
  ],
  imports: [
    CommonModule,
    MaterialModule,
    AddMealRoutingModule,
    ReactiveFormsModule
  ]
})
export class AddMealModule { }
