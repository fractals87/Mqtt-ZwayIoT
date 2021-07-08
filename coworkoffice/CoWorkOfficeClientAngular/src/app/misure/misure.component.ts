import { Component, OnInit } from '@angular/core';
import { MisureServiceService } from '../services/data/misure-service.service';
import { ActivatedRoute, Router } from '@angular/router';

export class Misure{
  constructor(
    public data: Date,
    public valore: number,
    public room: string,
    public iotdevice: string
  ) { }
}

export class ApiMsg {
  constructor(
    public code: string,
    public message: string
  ) {}
}

@Component({
  selector: 'app-misure',
  templateUrl: './misure.component.html',
  styleUrls: ['./misure.component.css']
})
export class MisureComponent implements OnInit {

  NumMis = 0;
  pagina = 1;
  righe = 10;

  apiMsg: ApiMsg;
  messaggio: string;

  filter: string = '';

  
  misura : Misure;
  misure : Misure[];

  constructor(private route:ActivatedRoute, private router: Router, private misureService: MisureServiceService) { }

  ngOnInit(): void {
    this.filter = this.route.snapshot.params["filter"];
    if (this.filter != undefined) {
      this.getMisure(this.filter);
    }
  }

  refresh() {
    this.getMisure(this.filter);
  }

  public getMisure(filter: string) {
    this.misureService.getMisureByDescription(filter).subscribe(
      response => {

        this.misure = [];

        console.log('Ricerchiamo misure con filtro ' + filter);

        this.misure = response;

        this.NumMis = this.misure.length
        console.log(this.misure.length);
        
      },
      error => {
        console.log(error.error);
        this.misure = [];
      }
    )
  }
}
