import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { ErrorComponent } from './error/error.component';
import { MisureComponent } from './misure/misure.component';
import { PrenotazioniComponent } from './prenotazioni/prenotazioni.component';
import { ManageprenotazioneComponent} from './manageprenotazione/manageprenotazione.component'

import {GatewaysComponent} from './gateways/gateways.component'
import {DeviceiotComponent} from './deviceiot/deviceiot.component'

import { LogoutComponent } from './logout/logout.component';
import { RouteGuardService } from './services/route-guard.service';
import { Ruoli } from './models/ruoli';
import { ForbiddenComponent } from './forbidden/forbidden.component';


const routes: Routes = [
  {path:'', component: LoginComponent},
  {path:'login', component: LoginComponent},
  {path:'logout', component: LogoutComponent},
  {path:'welcome/:userid', component: WelcomeComponent, canActivate:[RouteGuardService], data: { roles:[Ruoli.utente, Ruoli.admin]}},
  {path:'misure', component: MisureComponent, canActivate:[RouteGuardService], data: { roles:[Ruoli.admin]}},
  {path:'misure/:filter', component: MisureComponent, canActivate:[RouteGuardService], data: { roles:[Ruoli.admin]}},
  {path:'prenotazioni', component: PrenotazioniComponent, canActivate:[RouteGuardService], data: { roles:[Ruoli.utente, Ruoli.admin]}},
  {path:'manageprenotazione/:idPrenotazione', component: ManageprenotazioneComponent, canActivate:[RouteGuardService], data: { roles:[Ruoli.utente, Ruoli.admin]}},

  {path:'gateways', component: GatewaysComponent, canActivate:[RouteGuardService], data: { roles:[Ruoli.admin]}},
  {path:'deviceiot/:idGateway', component: DeviceiotComponent, canActivate:[RouteGuardService], data: { roles:[Ruoli.admin]}},

  {path:'forbidden', component: ForbiddenComponent},
  {path:'**', component: ErrorComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
