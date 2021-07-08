import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { server, port } from 'src/app/app-costants';
import { DeviceIot } from 'src/app/deviceiot/deviceiot.component';

@Injectable({
  providedIn: 'root'
})
export class DeviceiotService {

  constructor(private httpClient:HttpClient) { }

  getDeviceIot(id: string) {
    return this.httpClient.get<DeviceIot[]>(`http://${server}:${port}/api/IoTDevices?idGateway=${id}`);
  }
}
