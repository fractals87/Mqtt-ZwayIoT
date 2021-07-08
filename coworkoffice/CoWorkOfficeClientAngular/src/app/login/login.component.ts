import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthJWTService } from '../services/authJWTservice';

class ApiMsg {
  constructor(
    public code: string,
    public message: string
  ) {}
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userid = 'paolo.franzini@3p-ictsoftware.it'
  password ="Password1!"
  auth = null;
  errorMsg = ""
  successMsg = ""

  apiMsg: ApiMsg;

  //constructor(private route : Router, private Authapp: AuthappService) { }
  constructor(private route : Router, private Authapp: AuthJWTService) { }

  ngOnInit(): void {
  }

  gestAut(){
    //console.log(this.userid);

    this.Authapp.autenticaService(this.userid, this.password)
      .subscribe(
        data => {
          //console.log("TEST");
          //console.log(data);
          //console.log("TEST");          
          this.auth = true;
          this.route.navigate(['welcome', this.userid]);
        },
        error => {
          console.log(error);
          this.apiMsg = error.error;
          this.errorMsg = this.apiMsg.message;

          this.auth = false;
        }
      )
    }
}
