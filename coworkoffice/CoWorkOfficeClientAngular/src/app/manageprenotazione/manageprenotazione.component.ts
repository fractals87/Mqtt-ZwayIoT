import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Prenotazioni, Rooms, ApiMsg } from '../prenotazioni/prenotazioni.component';
import { PrenotazioniDataService } from '../services/data/prenotazioni-data.service';

@Component({
  selector: 'app-manageprenotazione',
  templateUrl: './manageprenotazione.component.html',
  styleUrls: ['./manageprenotazione.component.css']
})
export class ManageprenotazioneComponent implements OnInit {

  IdPrenotazione: string;

  prenotazione: Prenotazioni;

  Conferma: string;
  Errore: string;

  IsModifica: boolean = false;

  Err403Msg : string = "Non sei autorizzato";

  apiMsg: ApiMsg;

  rooms: Rooms[];

  constructor(private route:ActivatedRoute, private router:Router, private prenotazioniService: PrenotazioniDataService) { }

  ngOnInit(): void {
    this.prenotazione = new Prenotazioni(0, new Date().toLocaleString(), new Date().toLocaleString(), 0, 0, '' )
    this.IdPrenotazione = this.route.snapshot.params['idPrenotazione'];

    //Otteniamo i dati dell'Iva
    this.prenotazioniService.getRooms().subscribe(
      response => {
        this.rooms = response;
        //console.log(response);
      },
      error => {
        //console.log(error);
      }
    )

    if(this.IdPrenotazione!="-1"){
      this.IsModifica = true;
      //Otteniamo i dati dell'articolo
      this.prenotazioniService.getPrenotazioniById(this.IdPrenotazione).subscribe(
        response => {

          this.prenotazione = response;
          console.log(this.prenotazione); 
        },
        error => {
          console.log(error);
        }
      )
    }else{
      this.IsModifica = false;
    }
  }

  salva() {
    this.Conferma = '';
    this.Errore = '';
    
    console.log(this.prenotazione);
    if(this.IsModifica){    
      this.prenotazioniService.updPrenotazioni(this.prenotazione).subscribe(
        response => { 
          console.log(response);
          this.apiMsg = response;
          this.Conferma = this.apiMsg.message;
          console.log(this.Conferma);

          //this.router.navigate(['manageprenotazione',this.prenotazione.id]);
        },
        error => {
          console.log(error);
          this.apiMsg = error.error;
          this.Errore = (error.status== 403) ? this.Err403Msg :  this.apiMsg.message;
          
          console.log(this.Errore);    
        }
      )
    }else{
      this.prenotazioniService.insPrenotazioni(this.prenotazione).subscribe(
        response => { 
          console.log(response);
          this.apiMsg = response;
          this.Conferma = this.apiMsg.message;
          console.log(this.Conferma);
        },
        error => {
          console.log(error);
          this.apiMsg = error.error;
          this.Errore = (error.status== 403) ? this.Err403Msg :  this.apiMsg.message;
          console.log(this.Errore);    
        }
      )
    }
  }
}
