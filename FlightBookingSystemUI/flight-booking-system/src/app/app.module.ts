import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { ManageInventoryComponent } from './manage-inventory/manage-inventory.component';
import { BookFlightsComponent } from './book-flights/book-flights.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';
import { AuthService } from './services/auth.service';
import { SearchService } from './services/search.service';
import { AuthGuard } from './services/auth.guard';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { CancelBookingComponent } from './cancel-booking/cancel-booking.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    SearchFlightsComponent,
    ManageInventoryComponent,
    BookFlightsComponent,
    MyBookingsComponent,
    CancelBookingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [SearchService, AuthService, AuthGuard, {
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
