import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './shared/modules/material.module';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';
import { SandSoulModule } from 'sand-soul';

@NgModule({
    declarations: [AppComponent, PageNotFoundComponent],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        HttpClientModule,
        MaterialModule,
        StoreModule.forRoot({}),
        EffectsModule.forRoot([]),
        StoreDevtoolsModule.instrument({
            name: 'Calo DevTools',
            maxAge: 25,
            logOnly: environment.production,
        }),
        SandSoulModule
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
