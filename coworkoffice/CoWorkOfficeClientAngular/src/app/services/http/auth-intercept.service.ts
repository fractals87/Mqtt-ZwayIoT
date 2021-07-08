import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHeaders, HttpHandler } from '@angular/common/http';
import { AuthappService } from '../authapp.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptService implements HttpInterceptor {

  constructor(private BasicAuth: AuthappService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler) {
    
    //let UserId = "Paolo";
    //let Password = "Password11";
    //let AuthHeader = "Basic " + window.btoa(UserId + ":" + Password);

    let User = this.BasicAuth.loggedUser();
    let AuthToken = this.BasicAuth.getAuthToken();

    if(AuthToken && User){
    request = request.clone(
      {
        setHeaders :
        {
          Authorization: AuthToken
        }
      })
    }

    return next.handle(request);
  
  } 
}
