import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

export class FlightDetails {
    flightNo: string = '';
    flightName: string = '';
    fromLocation: string = '';
    toLocation: string = '';
    departureDateTime: string = '';
    arrivalDateTime: string = '';
    price: number = 0;
    noOfSeats: number = 0;
}

