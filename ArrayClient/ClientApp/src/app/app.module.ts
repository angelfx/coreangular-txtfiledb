import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { FileDataComponent } from './file-data/file-data.component';
import { ArrayDataComponent } from './file-data/array-data.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FileDataComponent,
    ArrayDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: FileDataComponent, pathMatch: 'full' },
      { path: 'file-data', component: FileDataComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
