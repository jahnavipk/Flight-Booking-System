import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookingHistoryDetails } from '../models/BookingModel';


@Component({
  selector: 'app-cancel-booking',
  templateUrl: './cancel-booking.component.html'
})
export class CancelBookingComponent implements OnInit {

  isBookingHistoryRequired: boolean = false;
  emailId: string = '';
  pnr: string = '';
  bookingHistoryDetails: BookingHistoryDetails = new BookingHistoryDetails();
  bookingHistoryDetailsArray: Array<BookingHistoryDetails> = new Array<BookingHistoryDetails>();

  constructor(public httpc: HttpClient, private router: Router) { }

  ngOnInit(): void {
  }

  getBookingHistoryByPnr() {
    this.isBookingHistoryRequired = true;
    const url = "http://localhost:48531/api/flight/booking/ticket/" + this.pnr;
    this.httpc.get(url).subscribe(res => this.Success(res), res => this.Error(res))
  }

  cancelBooking() {
    if (confirm("Are you sure you want to cancel the booking with PNR " + this.pnr + "?")) {
      const url = "http://localhost:48531/api/flight/booking/cancel/" + this.pnr;
      this.httpc.delete(url).subscribe(res => this.SuccessCancel(res), res => this.Error(res))
    }
  }

  Error(res: any) {
    console.log(res);
    alert(res.Response);
  }
  Success(res: any) {
    if (this.isBookingHistoryRequired)
      this.bookingHistoryDetailsArray = res;
  }
  
  SuccessCancel(res: any) {
    alert(res.Response);
    if (this.isBookingHistoryRequired)
      this.bookingHistoryDetailsArray = res;
  }

}
