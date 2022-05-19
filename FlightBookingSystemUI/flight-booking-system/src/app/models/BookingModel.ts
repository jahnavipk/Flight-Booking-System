import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

//Booking Service
export class PassengerDetails {
    passengerName: string = '';
    passengerAge: string = '';
    passengerGender: string = '';
    isMealOpted: string = '';
    price: number = 0;
}

//Booking Service
export class FlightBookingDetails {
    userId: number = 0;
    flightNo: string = '';
    noOfPassengers: number = 0;
    departureDateTime: string = '';
    isOneWay: string = '';
    returnDateTime: string = '';
    tblPassengerDetails: Array<PassengerDetails> = new Array<PassengerDetails>();
}

export class BookingHistoryDetails {
    pnr: number = 0;
    userName: string = '';
    flightNo: string = '';
    passengerName: string = '';
    passengerAge: number = 0;
    passengerGender: string = '';
    isMealOpted: string = '';
    mealType: string = '';
    departureDateTime: string = '';
    isOneWay: string = '';
    returnDateTime: string = '';
    noOfPassengers: number = 0;
    price: number = 0;
    statusCode: number = 0;
}