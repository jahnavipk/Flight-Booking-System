import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FlightDetails } from '../models/FlightDetails';
import { FlightInventoryDetails } from '../models/InventoryDetails';

@Component({
  selector: 'app-manage-inventory',
  templateUrl: './manage-inventory.component.html'
})
export class ManageInventoryComponent implements OnInit {

  flightData: FlightInventoryDetails = new FlightInventoryDetails();
  flightDataArray: Array<FlightInventoryDetails> = new Array<FlightInventoryDetails>();
  
  constructor(public httpc: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    this.flightData = { flightNo: "", flightName: "", fromLocation: "", toLocation: "", departureDate: "", departureTime: "", arrivalDate: "", arrivalTime: "", price: 0, noOfSeats: 0, mealOption: "Y", remarks: "" };
    this.flightDataArray.push(this.flightData);
  }

  addRow(index: any) {
    this.flightData = { flightNo: "", flightName: "", fromLocation: "", toLocation: "", departureDate: "", departureTime: "", arrivalDate: "", arrivalTime: "", price: 0, noOfSeats: 0, mealOption: "Y", remarks: "" };
    this.flightDataArray.push(this.flightData);
    console.log(this.flightDataArray);
    return true;
  }

  deleteRow(index: any) {
    if (this.flightDataArray.length == 1) {
      return false;
    } else {
      this.flightDataArray.splice(index, 1);
      return true;
    }
  }

  addInventory() {
    debugger;
    this.httpc.post("http://localhost:48531/api/flight/airline/inventory/add", this.flightDataArray).subscribe(res => { this.Success(res) }, res => this.Error);
  }

  Error(res: any) {
    console.log(res);
    alert(res.Response);

  }
  Success(res: any) {    
    this.flightDataArray = res;
    alert(res.Response);
  }

}
