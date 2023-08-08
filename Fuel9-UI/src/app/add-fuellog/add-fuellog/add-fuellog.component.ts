import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormControl, Validators, ValidatorFn } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FuelLogsService } from 'src/app/Services/fuel-logs.service';
import { FuelLogPost } from 'src/app/Models/fuelLogPost.model';

@Component({
  selector: 'app-add-fuellog',
  templateUrl: './add-fuellog.component.html',
  styleUrls: ['./add-fuellog.component.css']
})
export class AddFuellogComponent implements OnInit {
  vehicleId: number;
  fuelLog: FuelLogPost;
  atLeastOne: boolean = false;

  newFuellogForm= new FormGroup({
    amount: new FormControl("", Validators.required),
    price: new FormControl("", Validators.required),
    distance: new FormControl(),
    totalOdometer: new FormControl(),
    fuelType: new FormControl("", Validators.required),
    fullTank: new FormControl(),
    refuelTime: new FormControl()
  });

  fuelTypes = [
    "Diesel",
    "Diesel_with_additive",
    "Petrol",
    "E85",
    "E80",
    "Petrol_with_additives",
    "Petrol_95_octans",
    "Petrol_100_octans",
    "CNG",
    "LPG"
  ]


  constructor(private fuelLogService: FuelLogsService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {this.vehicleId = params['id']});
  }

  onSubmit(){
    this.fuelLog = {
      vehicleID: this.vehicleId,
      amount: this.newFuellogForm.controls['amount'].value,
      price: this.newFuellogForm.controls['price'].value,
      distance: this.newFuellogForm.controls['distance'].value ? this.newFuellogForm.controls['distance'].value : 0,
      totalOdometer: this.newFuellogForm.controls['totalOdometer'].value ? this.newFuellogForm.controls['totalOdometer'].value : 0,
      fuelType: this.newFuellogForm.controls['fuelType'].value,
      refuelTime: this.newFuellogForm.controls['refuelTime'].value,
      isFull: (Boolean)((<HTMLInputElement>document.getElementById('fullTank')).value)
    }
console.log(this.fuelLog);
    this.fuelLogService.addFuelLog(this.fuelLog).subscribe(() => {
      this.router.navigateByUrl('vehicle-fuellogs/' + this.vehicleId);
    }, () => {
      window.alert('Something went wrong with adding fuel log.');
    })
  }

  validate(){
    if(this.newFuellogForm.controls['distance'].value !== null || this.newFuellogForm.controls['totalOdometer'].value !== null ){
      this.atLeastOne = true
    } else {
      this.atLeastOne = false
    }
  }
}
