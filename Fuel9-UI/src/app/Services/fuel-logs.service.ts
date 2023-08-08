import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FuelLog } from '../Models/fuelLog.model';
import { environment } from 'src/environments/environment';
import { VehicleStats } from '../Models/vehicleStats.model';
import { FuelLogPost } from '../Models/fuelLogPost.model';

@Injectable({
  providedIn: 'root'
})
export class FuelLogsService {

  constructor(private http: HttpClient) { }

  addFuelLog(fuellog: FuelLogPost):Observable<void> {
    return this.http.post<void>(environment.serverURL + '/FuelLogs', fuellog);
  }

  getVehiclesFuelLogs(vehicleId: number) : Observable<FuelLog[]> {
    return this.http.get<FuelLog[]>(environment.serverURL + '/FuelLogs/' + vehicleId);
  }

  getStatistics(vehicleId: number) : Observable<VehicleStats> {
    return this.http.get<VehicleStats>(environment.serverURL + '/FuelLogs/statistics/' + vehicleId);
  }

  getFuelLog(fuelLogId:number):Observable<FuelLog>{
    return this.http.get<FuelLog>(environment.serverURL + '/FuelLogs/details/' + fuelLogId);
  }

  deleteFuelLog(fuelLogId:number):Observable<void>{
    return this.http.delete<void>(environment.serverURL + '/FuelLogs/' + fuelLogId);
  }

  updateFuelLog(fuelLog:FuelLog):Observable<FuelLog>{
    return this.http.patch<FuelLog>(environment.serverURL + '/FuelLogs/' + fuelLog.id?.toString(), fuelLog);
  }
}
