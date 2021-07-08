import { Component, OnInit } from '@angular/core';
import { AuthappService } from '../services/authapp.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  _authApp: AuthappService;

  constructor(private AuthApp: AuthappService) { 
    this._authApp = AuthApp;
  }

  ngOnInit(): void {
  }

}
