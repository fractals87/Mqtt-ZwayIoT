import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { port, server, authServerUri } from '../app-costants';

export class AuthData{
  constructor(
    public codice: string,
    public messaggio: string
  ){}
}

@Injectable({
  providedIn: 'root'
})
export class AuthJWTService {
  
  constructor(private httpClient:HttpClient) { }

  autenticaService(Email: string, Password: string) {
    console.log("AUTH JWT");
    let Abilitato = true;
    return this.httpClient.post<any>(
      `${authServerUri}`, 
      {Email,Password,Abilitato})
      .pipe(
        map(
          data => {
            sessionStorage.setItem("Utente", Email);
            sessionStorage.setItem("AuthToken", `Bearer ${data.token}`);
            return data ;
          }
        )
      );
  }

  getAuthToken(){
    if(this.loggedUser){
      return sessionStorage.getItem("AuthToken");
    }else{
      return "";
    }
  }

  loggedUser()
  {
    let utente = sessionStorage.getItem("Utente");
    return (sessionStorage.getItem("Utente")!=null)?utente:""
  }

  isLogged(){
    return (sessionStorage.getItem("Utente")!=null)?true:false
  }

  logOut(){
    sessionStorage.removeItem("Utente");
  }
}
