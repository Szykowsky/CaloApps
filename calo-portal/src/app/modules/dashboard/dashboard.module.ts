import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './container/dashboard.component';
import { DashboardRoutingModule } from './dashboard.routing.module';
import { MaterialModule } from 'src/app/shared/modules/material.module';



@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    MaterialModule
  ]
})
export class DashboardModule { }
