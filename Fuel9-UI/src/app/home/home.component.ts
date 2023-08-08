import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Vehicle } from '../Models/vehicle.model';
import { VehiclesService } from '../Services/vehicles.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  vehicles : Vehicle[] = [];

  constructor(private service: VehiclesService, private router: Router) { 
   
    this.service.getAllVehicles().subscribe(
      (data : Vehicle[]) => {
        this.vehicles = data;
      },
      error => {
          window.alert('Something went wrong.');
      }
    );
  }

  onFuelLogClick(vehicle: Vehicle): void{
    this.router.navigateByUrl('/vehicle-fuellogs/' + vehicle.id)
  }

  detailsClick(vehicle: Vehicle): void {
    this.router.navigateByUrl('/vehicle-details/' + vehicle.id);
  }
}
