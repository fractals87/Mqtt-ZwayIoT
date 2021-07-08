import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { LoginComponent } from './login/login.component';
import { ErrorComponent } from './error/error.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { JumbotronComponent } from './jumbotron/jumbotron.component';
import { LogoutComponent } from './logout/logout.component';

import { NgxPaginationModule } from 'ngx-pagination';
import { AuthInterceptService } from './services/http/auth-intercept.service';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { MisureComponent } from './misure/misure.component';
import { PrenotazioniComponent } from './prenotazioni/prenotazioni.component';
import { ManageprenotazioneComponent } from './manageprenotazione/manageprenotazione.component';

import { DeviceiotComponent } from './deviceiot/deviceiot.component';
import { GatewaysComponent } from './gateways/gateways.component';

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    LoginComponent,
    ErrorComponent,
    NavbarComponent,
    FooterComponent,
    JumbotronComponent,
    LogoutComponent,

    ForbiddenComponent,
    MisureComponent,
    PrenotazioniComponent,
    ManageprenotazioneComponent,
    DeviceiotComponent,
    GatewaysComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgxPaginationModule 
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS, useClass: AuthInterceptService, multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
