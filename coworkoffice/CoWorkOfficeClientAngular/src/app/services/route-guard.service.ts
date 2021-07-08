import { Injectable } from '@angular/core';
import { JwtHelperService} from '@auth0/angular-jwt';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthappService } from './authapp.service';
import { AuthJWTService } from './authJWTservice';


@Injectable({
  providedIn: 'root'
})
export class RouteGuardService implements CanActivate {

  token : string = '';
  ruoli : string[] = new Array();
  ruoliRequest  : string[] = new Array();
  items : any;

  //constructor(private AuthApp: AuthappService, private route: Router) { }
  constructor(private AuthApp: AuthJWTService, private route: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot){

    this.token = this.AuthApp.getAuthToken();
    const helper = new JwtHelperService();
    const decodeToken = helper.decodeToken(this.token);

    this.items =decodeToken['role'];
    if(!Array.isArray(this.items)){
      this.ruoli.push(this.items);
    }else{
      this.ruoli = this.items;
    }

    this.ruoliRequest = route.data.roles;

    //console.log(this.ruoli);
    //console.log(route.data.roles);
    //console.log(typeof(route.data.roles));
    //console.log(this.ruoli.some(r=> this.ruoliRequest.includes(r)));
    if(!this.AuthApp.isLogged()){
      this.route.navigate(["login"]);
      return false;
    }else{
      if(route.data.roles == null|| route.data.roles.length ===0){
        return true;
      }else if(this.ruoli.some(r=> this.ruoliRequest.includes(r))){
        return true;
      }else{
        this.route.navigate(['forbidden']);
        return false;
      }
    }
  }
}
