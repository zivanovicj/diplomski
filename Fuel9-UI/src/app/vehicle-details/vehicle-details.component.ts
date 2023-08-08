import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Vehicle } from '../Models/vehicle.model';
import { VehiclesService } from '../Services/vehicles.service';

@Component({
  selector: 'app-vehicle-details',
  templateUrl: './vehicle-details.component.html',
  styleUrls: ['./vehicle-details.component.css']
})
export class VehicleDetailsComponent implements OnInit {
  id: number = 0;
  vehicle:Vehicle;
  updatedVehicle:Vehicle;
  statsIsLoaded: boolean = false;

  updateVehicleForm = new FormGroup({
    Manufacturer: new FormControl('', Validators.required),
    YearOfProduction: new FormControl('', Validators.required),
    Model: new FormControl('', Validators.required),
    Odometer: new FormControl('', Validators.required),
    EnginePower: new FormControl('', Validators.required)
  });

  constructor(private service: VehiclesService, private route:ActivatedRoute, private router: Router) { 
    route.params.subscribe(params => { this.id = params['id']; });

    this.service.getVehicleById(this.id).subscribe(
      (data : Vehicle) => {
        if (data != null)
        {
          this.vehicle = data; 
          this.statsIsLoaded = true;  
          console.log(data);    
        }
        else
          window.alert('Something went wrong.');
      },
      error => {
          window.alert('Something went wrong.');
      }
    );   
  }

  ngOnInit(): void {
    setTimeout(() => this.setControlValuesFromNativeValues(), 400);
  }

  setControlValuesFromNativeValues(): void {
    this.updateVehicleForm.get('Manufacturer')?.setValue(this.vehicle.manufacturer);
    this.updateVehicleForm.get('YearOfProduction')?.setValue(this.vehicle.yearOfProduction);
    this.updateVehicleForm.get('Model')?.setValue(this.vehicle.model);
    this.updateVehicleForm.get('Odometer')?.setValue(this.vehicle.odometer);
    this.updateVehicleForm.get('OdometerType')?.setValue(this.vehicle.odometerType);
    this.updateVehicleForm.get('EnginePower')?.setValue(this.vehicle.enginePower);
    this.updateVehicleForm.get('VehicleType')?.setValue(this.vehicle.vehicleType);
    this.updateVehicleForm.get('FuelType')?.setValue(this.vehicle.fuelType);
    this.updateVehicleForm.get('Transmission')?.setValue(this.vehicle.transmission);
  }

  onSubmit() {
    this.updatedVehicle = {
      id:  this.vehicle.id,
      manufacturer: this.updateVehicleForm.controls['Manufacturer'].value,
      yearOfProduction: this.updateVehicleForm.controls['YearOfProduction'].value,
      model: this.updateVehicleForm.controls['Model'].value,
      odometer: this.updateVehicleForm.controls['Odometer'].value,
      enginePower: this.updateVehicleForm.controls['EnginePower'].value,
      odometerType: (document.getElementById('OdometerType') as HTMLInputElement).value,
      vehicleType: (document.getElementById('VehicleType') as HTMLInputElement).value,
      fuelType: (document.getElementById('FuelType') as HTMLInputElement).value,
      transmission: (document.getElementById('Transmission') as HTMLInputElement).value
    }
    console.log(this.updatedVehicle);

    this.service.updateVehicle(this.updatedVehicle).subscribe(
      (data : Vehicle) => {   
        if (data != null)    
          this.router.navigateByUrl('/home');
        else
          window.alert("Update vehicle failed");
      },
      error => {
        window.alert("Update vehicle failed");
      }
    )
  }

  onDelete() {
    if (window.confirm('Are you sure you want to delete vehicle?'))
    {
      this.service.deleteVehicle(this.id).subscribe(() => {
        this.router.navigateByUrl('');
      },() => {
            window.alert('Something went wrong.');
        });
    }  
  }    
}