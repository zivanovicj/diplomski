import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Vehicle } from '../Models/vehicle.model';
import { VehiclesService } from '../Services/vehicles.service';

@Component({
  selector: 'app-add-vehicle',
  templateUrl: './add-vehicle.component.html',
  styleUrls: ['./add-vehicle.component.css']
})
export class AddVehicleComponent {
  newVehicleForm=new FormGroup({
    manufacturer:new FormControl('', Validators.required),
    model:new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]),
    yearOfProduction:new FormControl('', Validators.required),
    enginePower:new FormControl('', [Validators.required, Validators.min(1), Validators.max(1000)]),
    odometer:new FormControl('', [Validators.required, Validators.min(0)]),
    odometerType:new FormControl('', Validators.required),
    vehicleType:new FormControl('', Validators.required),
    fuelType:new FormControl('', Validators.required),
    transmission:new FormControl('', Validators.required)
  });

  vehicleTypes = [
    "Car",
    "Bus",
    "Motorcycle",
    "Truck",
    "Scooter"
  ]

  transmissionTypes =[
    "Manual",
    "Automatic",
    "Semi Automatic"
  ]

  odometerUnits=[
    "Metric",
    "Imperial"
  ]

  fuelTypes=[
    "Diesel",
    "Petrol",
    "LPG"
  ]
  vehicle:Vehicle;

  constructor(private vehiclesService:VehiclesService, private router:Router) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this.vehicle ={
      manufacturer:this.newVehicleForm.controls['manufacturer'].value,
      model:this.newVehicleForm.controls['model'].value,
      yearOfProduction:this.newVehicleForm.controls['yearOfProduction'].value,
      enginePower:this.newVehicleForm.controls['enginePower'].value,
      odometer:this.newVehicleForm.controls['odometer'].value,
      odometerType:this.newVehicleForm.controls['odometerType'].value,
      vehicleType:this.newVehicleForm.controls['vehicleType'].value,
      fuelType:this.newVehicleForm.controls['fuelType'].value,
      transmission:this.newVehicleForm.controls['transmission'].value.replace(' ', '_')
    }

    this.vehiclesService.addVehicle(this.vehicle).subscribe(() => {
      this.router.navigateByUrl('');
    },() => {
          window.alert('Something went wrong.');
      });
  }

}
