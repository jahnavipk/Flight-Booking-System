import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

//Search Service
export class SearchInputData {
    fromLocation: string = '';
    toLocation: string = '';
    noOfPassengers: number = 0;
    departureDate: string = '';
    returnDate: string = '';
    formSearchGroup: FormGroup;//Create

    /**
     *
     */
    constructor() {
        var _builder = new FormBuilder();
        this.formSearchGroup = _builder.group({});

        //Control==>validation
        this.formSearchGroup.addControl("FromLocationValidator", new FormControl('', Validators.required));
        this.formSearchGroup.addControl("ToLocationValidator", new FormControl('', Validators.required));
        this.formSearchGroup.addControl("DepartureDateValidator", new FormControl('', Validators.required));
        //this.formSearchGroup.addControl("NoOfPassengersValidator", new FormControl('', Validators.required));


        // var validationcollection = [];
        // validationcollection.push(Validators.required);
        // validationcollection.push(Validators.pattern("^[0-9]+$"));

       //this.formSearchGroup.addControl("NoOfPassengersValidator", new FormControl('', Validators.compose(validationcollection)));
    }
}