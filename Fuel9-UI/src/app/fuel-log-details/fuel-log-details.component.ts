import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { FuelLog } from '../Models/fuelLog.model';
import { FuelLogsService } from '../Services/fuel-logs.service';
import { DatePipe, formatDate } from '@angular/common';

@Component({
  selector: 'app-fuel-log-details',
  templateUrl: './fuel-log-details.component.html',
  styleUrls: ['./fuel-log-details.component.css']
})

export class FuelLogDetailsComponent implements OnInit {
  fuelLogId:number;
  fuelLog:FuelLog;
  showInfo:boolean = false;
  datetime=this.datePipe.transform(new Date(), 'yyyy-MM-ddTHH:mm:ss');

  updateFuelLogForm=new FormGroup({
    amount:new FormControl('', Validators.required),
    price:new FormControl('', Validators.required),
    distance:new FormControl('', Validators.required),
    totalOdometer:new FormControl('', Validators.required)
  })

  fuelTypes = [
    "diesel",
    "diesel with additive",
    "petrol",
    "e85",
    "e80",
    "petrol with additives",
    "petrol 95 octans",
    "petrol 100 octans",
    "cng",
    "lpg"
  ]

  constructor(private route:ActivatedRoute, private fuelLogsService:FuelLogsService, private datePipe:DatePipe, private router:Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => { this.fuelLogId = params['id']; });

    this.fuelLogsService.getFuelLog(this.fuelLogId).subscribe( fuelLog => {
      this.fuelLog = fuelLog;
      this.fuelLog.fuelType = this.fuelLog.fuelType.split('_').join(' ').toLowerCase();
      this.showInfo = true;
      this.setValues();
    }, error => {
      window.alert("There was a problem loading fuel log details");
    });
  }

  setValues(){
    this.updateFuelLogForm.controls['amount'].setValue(this.fuelLog.amount);
    this.updateFuelLogForm.controls['price'].setValue(this.fuelLog.price);
    this.updateFuelLogForm.controls['distance'].setValue(this.fuelLog.distance);
    this.updateFuelLogForm.controls['totalOdometer'].setValue(this.fuelLog.totalOdometer);
  }

  onDelete(){
    if(!confirm("Are you sure you want to delete this fuel log?")){
      return;
    }
    this.fuelLogsService.deleteFuelLog(this.fuelLog.id).subscribe(() => {
      this.router.navigateByUrl('/vehicle-fuellogs/' + this.fuelLog.vehicleID);
    }, (error) => {
      window.alert("There was a problem deleting fuel log");
    });
  }

  onUpdate(){
    let updatedFuelLog:FuelLog = {
      id:this.fuelLog.id,
      vehicleID:this.fuelLog.vehicleID,
      amount: this.updateFuelLogForm.controls['amount'].value,
      price:this.updateFuelLogForm.controls['price'].value,
      distance:this.updateFuelLogForm.controls['distance'].value,
      totalOdometer:this.updateFuelLogForm.controls['totalOdometer'].value,
      fuelType:(<HTMLInputElement>document.getElementById('fuelType')).value,
      refuelTime:this.datePipe.transform(new Date((<HTMLInputElement>document.getElementById('refuelTime')).value), 'yyyy-MM-ddTHH:mm:ss') || ""
    }
    updatedFuelLog.fuelType=updatedFuelLog.fuelType.split(' ').join('_');
    console.log(updatedFuelLog);
    this.fuelLogsService.updateFuelLog(updatedFuelLog).subscribe(newFuelLog => {
      this.router.navigateByUrl('/vehicle-fuellogs/' + newFuelLog.vehicleID);
    }, (error) => {
      window.alert("there was a problem updating fuel log")
    })
  }
}
