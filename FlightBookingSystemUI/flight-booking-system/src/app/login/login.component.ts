import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserData } from '../models/UserData';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {

  loginUserData: UserData = new UserData();

  userId: string = '';

  constructor(private _auth: AuthService, private _router: Router) { }


  loginUser() {
    
    this._auth.loginUser(this.loginUserData).subscribe(res => {
      localStorage.setItem('userId', res.userId)
      localStorage.setItem('roleId', res.roleId)
      localStorage.setItem('token', res.token)
      localStorage.setItem('userEmailId', res.userEmailId)
      localStorage.setItem('userName', res.userName)
      localStorage.setItem('userContact', res.userContactNO)

      const redirectedFromSearch = localStorage.getItem('redirectedFromSearch');

      if (redirectedFromSearch == 'true') {

        this._router.navigate(['/bookflight'])
      }
      else {

        if (localStorage.getItem('roleId') == "2")
          this._router.navigate(['/search'])
        else if (localStorage.getItem('roleId') == "1")
          this._router.navigate(['/inventory'])
      }
    },
      err => console.log(err)
    )
  }
}
