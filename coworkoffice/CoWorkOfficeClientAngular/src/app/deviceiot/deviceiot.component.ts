import { Component, OnInit } from '@angular/core';
import { DeviceiotService } from '../services/data/deviceiot.service';
import { ActivatedRoute, Router } from '@angular/router';

export class DeviceIot{
  constructor(
    public description: string,
    public deviceType: string,
    public probeType: string,
    public registrationIdentifier: string,
    public room: string
  ) { }
}

@Component({
  selector: 'app-deviceiot',
  templateUrl: './deviceiot.component.html',
  styleUrls: ['./deviceiot.component.css']
})
export class DeviceiotComponent implements OnInit {

  idGateway: string;

  NumD = 0;
  pagina = 1;
  righe = 5;

  devicesiot : DeviceIot[];

  constructor(private route:ActivatedRoute, private router: Router, private deviceiotDataService: DeviceiotService) { }

  ngOnInit(): void {
    this.idGateway = this.route.snapshot.params['idGateway'];
    this.getDeviceIot(this.idGateway);
  }

  public getDeviceIot(idGateway:string) {
    this.deviceiotDataService.getDeviceIot(idGateway).subscribe(
      response => {

        this.devicesiot = [];

        this.devicesiot = response;

        this.NumD = this.devicesiot.length
        
      },
      error => {
        console.log(error.error);
        this.devicesiot = [];
      }
    )
  }
}
