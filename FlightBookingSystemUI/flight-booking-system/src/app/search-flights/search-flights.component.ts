import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, Injectable, OnInit } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { FlightDetails, SearchInputData } from '../models/FlightDetails';
import { AuthService } from '../services/auth.service';
import { SearchService } from '../services/search.service';

@Injectable()

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html'
})

export class SearchFlightsComponent {

  searchInputData: SearchInputData = new SearchInputData();
  flightDetails: Array<FlightDetails> = new Array<FlightDetails>();



  constructor(public httpc: HttpClient, private router: Router, private _searchService: SearchService, private _authService: AuthService) {
  }

  ngOnInit(): void {
    //this._searchService.getSearchResults().subscribe(res => this.flightDetails = res, err => console.log(err))
  }

  searchFlights() {
    debugger;
    var searchdto = {
      fromLocation: this.searchInputData.fromLocation,
      toLocation: this.searchInputData.toLocation,
      noOfPassengers: this.searchInputData.noOfPassengers,
      departureDate:this.searchInputData.departureDate,
      returnDate:this.searchInputData.returnDate
    }
    this.httpc.post("http://localhost:48531/api/flight/search", searchdto).subscribe(res => { this.Success(res) }, res => this.Error);

  }

  bookFlight(obj: FlightDetails) {
    this._searchService.UserBookingObj(obj);
    this.router.navigate(['/bookflight']); 
      
  }

  Error(res: any) {
    console.log(res);
  }
  Success(res: any) {
    console.log(res);    
    this.flightDetails = res;
    console.log(this.flightDetails);
  }

  userLoggedIn(input:boolean):boolean{
    if(input){
      return this._authService.userLoggedIn();
    }
    else{
      return !this._authService.userLoggedIn();
    }
  }

}


