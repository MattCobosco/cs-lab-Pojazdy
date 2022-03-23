using System;
using System.Runtime.Serialization;
using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class AirVehicle : IVehicle
    {
        public AirVehicle(int horsepower, bool hasEngine, IVehicle.FuelType fuelType)
        {
            Horsepower = horsepower;
            HasEngine = hasEngine;
            FuelType = fuelType;
            // air vehicle spawns on the ground
            CurrentEnvironment = IEnvironment.Environments.LandEnvironment;
            // air vehicle spawns stationary
            State = IVehicle.VehicleState.Stationary;
        }

        public int Speed { get; set; }
        public static IEnvironment CurrentEnvironment { get; private set; }
        public IVehicle.FuelType FuelType { get; }
        public int Horsepower { get; }
        public IVehicle.VehicleState State { get; set; }
        public bool HasEngine { get; }

        public void IncreaseSpeed(int change)
        {
            if (Speed + change <= CurrentEnvironment.MaxSpeed) 
                Speed += change;

            if (Speed > 0)
            {
                State = IVehicle.VehicleState.Moving;
            }
            
            // TODO: fix instant takeoff issue
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment) return;
            if (Speed / 3.6 >= IEnvironment.Environments.AirEnvironment.MinSpeed)
                Speed = (int) (Speed / 3.6);
            CurrentEnvironment = IEnvironment.Environments.AirEnvironment; // takeoff
        }

        public void DecreaseSpeed(int change)
        {
        }

        public override string ToString()
        {
            var vehicle = (IVehicle)this;
            var unit = CurrentEnvironment.Unit;
            return $"Type: Air Vehicle, Has Engine: {HasEngine}, " +
                   $"Current Environment: {CurrentEnvironment.Type}, " +
                   $"State: {vehicle.State}, Min Speed: {CurrentEnvironment.MinSpeed} {unit}, " +
                   $"Max Speed: {CurrentEnvironment.MaxSpeed} {unit}, Speed: {vehicle.GetConvertedSpeed(unit, Speed)} {unit}, " +
                   $"Horsepower: {Horsepower}, Fuel Type: {FuelType}";
        }
    }
}