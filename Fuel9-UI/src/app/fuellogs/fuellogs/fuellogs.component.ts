import { Component, OnInit } from '@angular/core';
import { FuelLog } from 'src/app/Models/fuelLog.model';
import { VehicleStats } from 'src/app/Models/vehicleStats.model';
import { FuelLogsService } from 'src/app/Services/fuel-logs.service';
import { ActivatedRoute, Router } from '@angular/router';
import { VehiclesService } from 'src/app/Services/vehicles.service';
import { Vehicle } from 'src/app/Models/vehicle.model';

@Component({
  selector: 'app-fuellogs',
  templateUrl: './fuellogs.component.html',
  styleUrls: ['./fuellogs.component.css']
})
export class FuellogsComponent {
  id: number = 0;
  tds: string[] = [ 
    'Fuel type',
    'Amount(liters)',
    'Price',
    'Refuel Date',
    'Actions'
   ]
  fuelLogs: FuelLog[] = [];
  vehicleStats!: VehicleStats;
  statsIsLoaded: boolean = false;
  unit:string;

  constructor(private fuelLogServices: FuelLogsService, private route: ActivatedRoute, private router:Router, private vehiclesService:VehiclesService) { 
    this.route.params.subscribe(params => { this.id = params['id']; });

    this.fuelLogServices.getVehiclesFuelLogs(this.id)
    .subscribe((fuellogs) => { this.fuelLogs = fuellogs; },
    error => {
      window.alert("Something went wrong while fetching fuellogs.")
    });

    this.fuelLogServices.getStatistics(this.id)
    .subscribe((vehicleStats) => { this.vehicleStats = vehicleStats;
                                   this.statsIsLoaded = true; },
    error =>{ 
      window.alert("Something went wrong while fetching statistics.")
    });

    this.vehiclesService.getVehicleById(this.id)
                        .subscribe(vehicle => {
                          this.unit = vehicle.odometerType.toLowerCase() === 'imperial' ? "mi" : "km";
                        })
  }

  onFuelLogDetails(id:number):void{
    this.router.navigateByUrl('/FuelLog/' + id);
  }

  clickAddFuellog(){
    this.router.navigateByUrl('vehicle-fuellogs/'+ this.id + '/add-fuellog');
  }
} 
