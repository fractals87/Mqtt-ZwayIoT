import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-jumbotron',
  templateUrl: './jumbotron.component.html',
  styleUrls: ['./jumbotron.component.css']
})
export class JumbotronComponent implements OnInit {

  //Variabili che vengono passate da un'altro controllo padre
  @Input() Titolo: string
  @Input() SottoTitolo: string
  @Input() Show: boolean = true
  
  constructor() { }

  ngOnInit(): void {
  }

}
