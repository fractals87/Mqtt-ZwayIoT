import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { port, server } from '../app-costants';

export class AuthData{
  constructor(
    public codice: string,
    public messaggio: string
  ){}
}

@Injectable({
  providedIn: 'root'
})
export class AuthappService {
  
  constructor(private httpClient:HttpClient) { }

  //server = "localhost";
  //port = "5001";
/*
  autentica(UserId, Password){
    if(UserId === "Paolo" && Password === "Password11")
    {
      sessionStorage.setItem("Utente",UserId);
      return true;
    }
    else
    {
      return false;
    }
  }
  */

  autenticaService(UserId: string, Password: string) {
    let AuthString = "Basic " + window.btoa(UserId + ":" + Password);

    let headers = new HttpHeaders(
      {Authorization:   AuthString }
    )

    return this.httpClient.get<AuthData>(
      `http://${server}:${port}/login`, 
      {headers})
      .pipe(
        map(
          data => {
            sessionStorage.setItem("Utente", UserId);
            sessionStorage.setItem("AuthToken", AuthString);
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
