import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Vehicle } from "../Models/vehicle.model";
import { environment } from "src/environments/environment";

const httpOptions={
  headers:new HttpHeaders({
    'Content-Type':'application/json'
  })
}

@Injectable({
    providedIn: 'root'
  })

export class VehiclesService{

    constructor( private http: HttpClient) { }

    getAllVehicles() : Observable<Vehicle[]> {
        return this.http.get<Vehicle[]>(environment.serverURL + '/Vehicles');
      }

    addVehicle(vehicle:Vehicle):Observable<void>{
      return this.http.post<void>(environment.serverURL + '/Vehicles', vehicle);
    }

    getVehicleById(id: number) : Observable<Vehicle> {
      return this.http.get<Vehicle>(environment.serverURL + '/Vehicles/' + id);
    }

    updateVehicle(vehicle:Vehicle):Observable<Vehicle> {
      return this.http.patch<Vehicle>(environment.serverURL + '/Vehicles', vehicle, httpOptions);
    }

    deleteVehicle(vehicleId:number):Observable<void> {
      return this.http.delete<void>(environment.serverURL + '/Vehicles/' + vehicleId);
    }
}

