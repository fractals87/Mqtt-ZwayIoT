import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Misure, ApiMsg } from 'src/app/misure/misure.component';
import { server, port } from 'src/app/app-costants';

@Injectable({
  providedIn: 'root'
})
export class MisureServiceService {

  constructor(private httpClient:HttpClient) { }

  getMisureByDescription(descrizione : string) {
    return this.httpClient.get<Misure[]>(`http://${server}:${port}/api/Measures?descrizione=${descrizione}`); //ALT + 0096 | ALT GR + '
  }

}
