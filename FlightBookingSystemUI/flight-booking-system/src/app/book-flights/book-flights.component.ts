import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightBookingDetails, FlightDetails, PassengerDetails } from '../models/FlightDetails';
import { SearchService } from '../services/search.service';

@Component({
  selector: 'app-book-flights',
  templateUrl: './book-flights.component.html'
})
export class BookFlightsComponent {

  @Input() userFlightData: FlightDetails = new FlightDetails();;

  constructor(private _search: SearchService, private _router: Router, private route: ActivatedRoute, public httpc: HttpClient) { }

  //userBookingDetailsArray: Array<FlightBookingDetails> = [];

  userBookingDetailsArray: any = [];
  passengerArray: Array<PassengerDetails> = [];
  newPassenger: any = {};
  userBookingData: any = {};

  ngOnInit(): void {
    debugger;

    this._search.obj.subscribe(data => {
      this.userFlightData = data;
    })

    this.newPassenger = { passengerName: "", passengerAge: "", passengerGender: "", isMealOpted: "", price: this.userFlightData.price };
    this.passengerArray.push(this.newPassenger);
  }

  addRow(index: any) {
    this.newPassenger = { passengerName: "", passengerAge: "", passengerGender: "", isMealOpted: "", price: this.userFlightData.price };
    this.passengerArray.push(this.newPassenger);
    return true;
  }

  deleteRow(index: any) {
    if (this.passengerArray.length == 1) {
      return false;
    } else {
      this.passengerArray.splice(index, 1);
      return true;
    }
  }

  bookFlight() {
    console.log(this.passengerArray);
    this.userBookingData = { userId: localStorage.getItem('userId'), flightNo: this.userFlightData.flightNo, noOfPassengers: this.passengerArray.length, departureDateTime: this.userFlightData.departureDateTime, isOneWay: "", returnDateTime: "2022-05-29T00:00:00", tblPassengerDetails: this.passengerArray }
    this.userBookingDetailsArray.push(this.userBookingData);
    console.log(this.userBookingDetailsArray);
    this.httpc.post("http://localhost:48531/api/flight/booking", this.userBookingDetailsArray).subscribe(res => { this.Success(res) }, res => this.Error);
  }

  Error(res: any) {
    console.log(res);
  }
  Success(res: any) {
    console.log(res.response);
    alert(res.response);
  }
}
