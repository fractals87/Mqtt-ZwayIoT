import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SalutiDataService {

  constructor(private httpClient:HttpClient) { }

  getSaluti(){
    //console.log("Ciao");
    return this.httpClient.get("http://localhost:5001/api/saluti/Paolo");
  }


}
