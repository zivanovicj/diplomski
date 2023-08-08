import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddVehicleComponent } from './add-vehicle/add-vehicle.component';
import { HomeComponent } from './home/home.component';
import { FuellogsComponent } from './fuellogs/fuellogs/fuellogs.component';
import { AddFuellogComponent } from './add-fuellog/add-fuellog/add-fuellog.component';
import { VehicleDetailsComponent } from './vehicle-details/vehicle-details.component';
import { FuelLogDetailsComponent } from './fuel-log-details/fuel-log-details.component';

const routes: Routes = [
  {path:'',redirectTo:'/home',pathMatch:'full'},
  {
    path: 'home', component: HomeComponent
  },
  { 
    path: 'addVehicle', component:AddVehicleComponent
  },
  {
    path: 'vehicle-fuellogs/:id', component: FuellogsComponent
  },
  {
    path:'FuelLog/:id', component : FuelLogDetailsComponent
  },
  {
    path: 'vehicle-details/:id', component: VehicleDetailsComponent
  },
  {
    path: 'vehicle-fuellogs/:id/add-fuellog', component:AddFuellogComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
