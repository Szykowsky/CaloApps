import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { NgModule } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';

@NgModule({
    imports: [
        MatToolbarModule,
        MatIconModule,
        MatButtonModule,
        MatCardModule,
        MatGridListModule
    ],
    exports: [
        MatToolbarModule,
        MatIconModule,
        MatButtonModule,
        MatCardModule,
        MatGridListModule
    ]
})
export class MaterialModule {}