import { Component, Injectable } from '@angular/core';
import { AuthService } from './services/auth.service';

@Injectable()
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'flight-booking-system';
  UserName = localStorage.getItem('userName');

  /**
   *
   */
  constructor(private _authService: AuthService) {


  }

  LogOut() {
    this._authService.logoutUser();
  }

  LoggedIn(input:boolean):boolean{
    if(input){
      return this._authService.loggedIn();
    }
    else{
      return !this._authService.loggedIn();
    }
  }

  userLoggedIn(input:boolean):boolean{
    if(input){
      return this._authService.userLoggedIn();
    }
    else{
      return !this._authService.userLoggedIn();
    }
  }

  adminLoggedIn(input:boolean):boolean{
    if(input){
      return this._authService.adminLoggedIn();
    }
    else{
      return !this._authService.adminLoggedIn();
    }

  }
}
