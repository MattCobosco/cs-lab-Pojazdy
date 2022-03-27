using System;
using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class AirVehicle : IVehicle
    {
        // General vehicle properties
        // An air vehicle => can switch environments between the air and the ground
        public IVehicle.VehicleType Type { get; set; } = IVehicle.VehicleType.AirVehicle;
        // Spawns on land
        public IEnvironment CurrentEnvironment { get; set; } = IEnvironment.Environments.LandEnvironment;
        // Native environment is air => the unit for speed and speed changes is m/s
        public IEnvironment NativeEnvironment { get; set; } = IEnvironment.Environments.AirEnvironment;
        public double Speed { get; set; }
        public bool HasEngine { get; set; }
        public int HorsePower { get; set; }
        public IVehicle.FuelType FuelUsed { get; set; }
        // Spawns stationary
        public IVehicle.State VehicleState { get; set; } = IVehicle.State.Stationary;

        protected AirVehicle(bool hasEngine, int horsePower, IVehicle.FuelType fuelType)
        {
            HasEngine = hasEngine;
            if (HasEngine)
            {
                HorsePower = horsePower;
                FuelUsed = fuelType;
            }
            else
            {
                // If no engine => horsepower is 0, fuel type is none
                HorsePower = 0;
                FuelUsed = IVehicle.FuelType.None;
            }
        }

        public void Accelerate(double changeInMps)
        {
            // A stationary vehicle cannot accelerate
            if (VehicleState == IVehicle.State.Stationary) return;
            // Increase speed but not above the maximum speed of air environment
            Speed = Math.Min(IEnvironment.Environments.AirEnvironment.MaxSpeed, Speed + changeInMps);
            // If vehicle is already in air don't check takeoff requirements
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment) return;
            // Takeoff if takeoff speed is reached
            if (Speed >= IEnvironment.Environments.AirEnvironment.MinSpeed)
                CurrentEnvironment = IEnvironment.Environments.AirEnvironment;
        }

        public void Decelerate(double changeInMps)
        {
            // A stationary vehicle cannot decelerate
            if (VehicleState == IVehicle.State.Stationary) return;
            // Decrease speed but not below the minimum speed of land environment
            Speed = Math.Max(IEnvironment.Environments.LandEnvironment.MinSpeed, Speed - changeInMps);
            // If vehicle is already on land don't check landing requirements
            if (CurrentEnvironment == IEnvironment.Environments.LandEnvironment) return;
            // Land if landing speed is reached
            if (Speed <= IEnvironment.Environments.AirEnvironment.MinSpeed)
                CurrentEnvironment = IEnvironment.Environments.LandEnvironment;
        }

        public override string ToString()
        {
            IVehicle vehicle = this;
            var nativeUnit = vehicle.NativeEnvironment.Unit;
            var currentUnit = vehicle.CurrentEnvironment.Unit;

            return $"Data Type: {GetType().Name}, " +
                   $"Vehicle Type: {vehicle.Type}, " +
                   $"Current Environment: {CurrentEnvironment.Type}, " +
                   $"Current State: {vehicle.VehicleState}, " +
                   $"Environment Min Speed: {CurrentEnvironment.MinSpeed} {currentUnit}, " +
                   $"Environment Max Speed: {CurrentEnvironment.MaxSpeed} {currentUnit}, " +
                   $"Current Speed: {IVehicle.GetConvertedSpeed(nativeUnit, currentUnit, vehicle.Speed):0.00} {currentUnit}, " +
                   "ADDITIONAL: " +
                   $"HasEngine: {vehicle.HasEngine}, " +
                   $"Horsepower: {vehicle.HorsePower}, " +
                   $"Type of Fuel: {vehicle.FuelUsed}";
        }
    }
}