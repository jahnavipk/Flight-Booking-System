import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { IBookingInterface } from '../interfaces/BookingInterface';
import { FlightDetails } from '../models/FlightDetails';
import { UserData } from '../models/UserData';

@Injectable({
    providedIn: 'root'
})
export class SearchService {

    UserBookingData: FlightDetails = new FlightDetails();

    private namesource = new BehaviorSubject<FlightDetails>(this.UserBookingData);
    obj = this.namesource.asObservable()

    constructor(private http: HttpClient, private router: Router) {
    }

    UserBookingObj(obj: FlightDetails) {
        this.namesource.next(obj);
    }

    saveDataInSession(obj: FlightDetails) {
        this.namesource.next(obj);
    }

    getDataFromSession() {
        debugger;
        console.log(this.UserBookingData);
        return this.UserBookingData;
    }
}