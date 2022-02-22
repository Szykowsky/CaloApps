import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddDietComponent } from './container/add-diet.component';
import { AddDietRoutingModule } from './add-diet.routing.module';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { AddDietFormComponent } from './components/add-diet-form/add-diet-form.component';

@NgModule({
  declarations: [AddDietComponent, AddDietFormComponent],
  imports: [
    CommonModule,
    MaterialModule,
    AddDietRoutingModule
  ]
})
export class AddDietModule { }
