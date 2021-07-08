import { Component, OnInit } from '@angular/core';
import { PrenotazioniDataService } from '../services/data/prenotazioni-data.service';
import { ActivatedRoute, Router } from '@angular/router';

export class Prenotazioni{
  constructor(
    public id: number,
    public da: string,
    public a: string,
    public clienti: number,
    public idroom: number,
    public room: string
  ) { }
}

export class ApiMsg {
  constructor(
    public code: string,
    public message: string
  ) {}
}

export class Rooms{
  constructor(
    public id: string,
    public description: string
  ){ }
}

@Component({
  selector: 'app-prenotazioni',
  templateUrl: './prenotazioni.component.html',
  styleUrls: ['./prenotazioni.component.css']
})
export class PrenotazioniComponent implements OnInit {

  NumMis = 0;
  pagina = 1;
  righe = 10;

  messaggio: string;

  filter: string = '';

  apiMsg: ApiMsg;

  Conferma: string;
  Errore: string;
  Err403Msg : string = "Non sei autorizzato";
  
  prenotazione : Prenotazioni;
  prenotazioni : Prenotazioni[];

  constructor(private route:ActivatedRoute, private router: Router, private prenotazioniDataService: PrenotazioniDataService) { }

  ngOnInit(): void {
    this.getPrenotazioni();
  }

  refresh() {
    this.getPrenotazioni();
  }

  public getPrenotazioni() {
    this.prenotazioniDataService.getPrenotazioni().subscribe(
      response => {

        this.prenotazioni = [];

        console.log('Ricerchiamo prenotazioni');

        this.prenotazioni = response;

        this.NumMis = this.prenotazioni.length
        console.log(this.prenotazioni.length);
        
      },
      error => {
        console.log(error.error);
        this.prenotazioni = [];
      }
    )
  }

  public deletePrenotazione(id: string){
    this.prenotazioniDataService.deletePrenotazione(id).subscribe(
      response => {
        console.log(response);
        this.apiMsg = response;
        this.Conferma = this.apiMsg.message;
        console.log(this.Conferma);
        this.refresh();
      },
      error => {
        console.log(error);
        this.apiMsg = error.error;
        this.Errore = (error.status== 403) ? this.Err403Msg :  this.apiMsg.message;
        
        console.log(this.Errore);    
      }
    )
  }

  Modifica(id: string) {
    console.log(`Modifica prenotazione ${id}`);
    this.router.navigate(['manageprenotazione', id]);
  }


}
