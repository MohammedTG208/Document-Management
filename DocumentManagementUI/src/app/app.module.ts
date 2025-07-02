import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './home/header/header.component';
import { CradComponent } from './crad/crad.component';
import { DocumentInfoDialogComponentComponent } from './home/document-info-dialog-component/document-info-dialog-component.component';
import { AddMessageComponent } from './home/document-info-dialog-component/add-message/add-message.component';

@NgModule({
  // No declarations for standalone components
  declarations: [],

  // Import standalone components directly
  imports: [
    BrowserModule,
    AppRoutingModule,
    HeaderComponent,
    CradComponent,
    DocumentInfoDialogComponentComponent,
    AddMessageComponent
  ],

  providers: [],

  // AppComponent must go in bootstrap (not imports)
  bootstrap: [AppComponent]
})
export class AppModule { }
