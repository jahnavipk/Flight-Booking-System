import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";

export class NewUserData {
    userName: string = '';
    emailId: string = '';
    password: string = '';
    contactNo: string = '';
    gender: string = '';
    age: number = 0;
    roleId: number = 2;

}