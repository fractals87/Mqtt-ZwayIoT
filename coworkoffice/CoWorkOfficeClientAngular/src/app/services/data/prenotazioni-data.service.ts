import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { server, port } from 'src/app/app-costants';
import { Prenotazioni, Rooms, ApiMsg } from 'src/app/prenotazioni/prenotazioni.component';

@Injectable({
  providedIn: 'root'
})
export class PrenotazioniDataService {

  constructor(private httpClient:HttpClient) { }

  getPrenotazioni() {
    return this.httpClient.get<Prenotazioni[]>(`http://${server}:${port}/api/Reservations`);
  }
  getPrenotazioniById(id: string) {
    return this.httpClient.get<Prenotazioni>(`http://${server}:${port}/api/Reservations/${id}`);
  }

  getRooms(){
    return this.httpClient.get<Rooms[]>(`http://${server}:${port}/api/Rooms`);
  }

  updPrenotazioni(prenotazione: Prenotazioni) {
    return this.httpClient.put<ApiMsg>(`http://${server}:${port}/api/Reservations/${prenotazione.id}`, prenotazione);
  }

  insPrenotazioni(prenotazione: Prenotazioni){
    return this.httpClient.post<ApiMsg>(`http://${server}:${port}/api/Reservations`, prenotazione);
  }

  deletePrenotazione(id: string){
    return this.httpClient.delete<ApiMsg>(`http://${server}:${port}/api/Reservations/${id}`);
  }
}
