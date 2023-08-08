export interface FuelLogPost{
    vehicleID: number;
    refuelTime: string;
    price: number;
    amount: number;
    fuelType: string;
    distance: number;
    totalOdometer: number;
    isFull: boolean;
}