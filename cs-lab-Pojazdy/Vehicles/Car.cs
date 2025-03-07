﻿using ClassLibrary.Vehicles;

namespace Vehicles;

public class Car : LandVehicle
{
    private readonly IVehicle _vehicle;
    
    public Car(int horsePower, IVehicle.FuelType fuelType) : base(true, horsePower, fuelType, 4)
    {
        _vehicle = this;
    }
    
    public void Start() => _vehicle.Start();

    public void Stop() => _vehicle.Stop();
    
    public void Accelerate(double speedChange) => _vehicle.Accelerate(speedChange);
    
    public void Decelerate(double speedChange) => _vehicle.Decelerate(speedChange);
}