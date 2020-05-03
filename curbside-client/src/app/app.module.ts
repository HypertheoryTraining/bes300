import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CurbsideComponent } from './components/curbside/curbside.component';
import { CurbsideHubService } from './services/curbside.hub.service';

@NgModule({
  declarations: [
    AppComponent,
    CurbsideComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [CurbsideHubService],
  bootstrap: [AppComponent]
})
export class AppModule { }
