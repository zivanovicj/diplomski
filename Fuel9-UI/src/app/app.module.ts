import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VehiclesService } from './Services/vehicles.service';
import { AddVehicleComponent } from './add-vehicle/add-vehicle.component';
import { FuellogsComponent } from './fuellogs/fuellogs/fuellogs.component';
import { FuelLogDetailsComponent } from './fuel-log-details/fuel-log-details.component';
import { DatePipe } from '@angular/common';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';
import { AddFuellogComponent } from './add-fuellog/add-fuellog/add-fuellog.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AddVehicleComponent,
    FuellogsComponent,
    VehicleDetailsComponent,
    FuelLogDetailsComponent,
    AddFuellogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [
    VehiclesService,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
