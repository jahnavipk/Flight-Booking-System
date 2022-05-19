import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookFlightsComponent } from './book-flights/book-flights.component';
import { CancelBookingComponent } from './cancel-booking/cancel-booking.component';
import { LoginComponent } from './login/login.component';
import { ManageInventoryComponent } from './manage-inventory/manage-inventory.component';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';
import { RegisterComponent } from './register/register.component';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { AuthGuard } from './services/auth.guard';

const routes: Routes = [

  {
    path: '',
    component: SearchFlightsComponent
  },
  {
    path: 'search',
    component: SearchFlightsComponent
  },
  {
    path: 'inventory',
    canActivate:[AuthGuard],
    component: ManageInventoryComponent
  },
  {
    path: 'bookflight',
    canActivate:[AuthGuard],
    component: BookFlightsComponent
  },  
  {
    path: 'my-bookings',
    canActivate:[AuthGuard],
    component: MyBookingsComponent
  },  
  {
    path: 'cancel-booking',
    canActivate:[AuthGuard],
    component: CancelBookingComponent
  }, 
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
