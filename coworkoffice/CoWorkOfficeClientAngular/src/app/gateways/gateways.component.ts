import { Component, OnInit } from '@angular/core';
import { GatewaysDataService } from '../services/data/gateways-data.service';
import { ActivatedRoute, Router } from '@angular/router';

export class Gateways{
  constructor(
    public id: string,
    public description: string,
    public protocol: string,
    public ip: string,
    public port: string,
    public user: string,
    public password: string
  ) { }
}

@Component({
  selector: 'app-gateways',
  templateUrl: './gateways.component.html',
  styleUrls: ['./gateways.component.css']
})

export class GatewaysComponent implements OnInit {

  NumG = 0;
  pagina = 1;
  righe = 10;

  gateways : Gateways[];

  constructor(private route:ActivatedRoute, private router: Router, private gatewaysDataService: GatewaysDataService) { }

  ngOnInit(): void {
    this.getGateways();
  }

  public getGateways() {
    this.gatewaysDataService.getGateways().subscribe(
      response => {

        this.gateways = [];

        this.gateways = response;

        this.NumG = this.gateways.length
        
      },
      error => {
        console.log(error.error);
        this.gateways = [];
      }
    )
  }

  VisDevIoT(id: string) {
    this.router.navigate(['deviceiot', id]);
  }

  Configure() {
    this.gatewaysDataService.Configure().subscribe(
      response => {
        
      },
      error => {
        console.log(error.error);
        this.gateways = [];
      }
    )
  }
}
