import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { server, port } from 'src/app/app-costants';
import { Gateways } from 'src/app/gateways/gateways.component';

@Injectable({
  providedIn: 'root'
})
export class GatewaysDataService {

  constructor(private httpClient:HttpClient) { }

  getGateways() {
    return this.httpClient.get<Gateways[]>(`http://${server}:${port}/api/Gateways`);
  }

  Configure() {
    return this.httpClient.get<Gateways[]>(`http://${server}:${port}/api/Gateways/Configure`);
  }
}
