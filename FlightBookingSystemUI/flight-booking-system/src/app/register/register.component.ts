import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NewUserData } from '../models/NewUserData';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {

  registerUserData: NewUserData = new NewUserData();

  constructor(private _auth: AuthService, private _router: Router) { }

  ngOnInit(): void {
  }

 

  registerUser() {
    debugger;    
    this._auth.registerUser(this.registerUserData).subscribe(res => {
      //localStorage.setItem('token', res.token)
      alert(res.response+". Please login to book a flight!")
      this._router.navigate(['/login'])
    },
      err => console.log(err)
    )
  }
}
