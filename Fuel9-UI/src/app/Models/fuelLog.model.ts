export interface FuelLog{
    id: number;
    vehicleID: number;
    refuelTime: string;
    price: number;
    amount: number;
    fuelType: string;
    distance: number;
    totalOdometer: number;
}