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
            // air vehicle spawns as stationary
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
            // if vehicle is in air => check max speed boundary and increase speed
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment)
            {
                if (Speed + change <= IEnvironment.Environments.AirEnvironment.MaxSpeed)
                {
                    Speed += change;
                }

                return;
            }

            Speed += change;
            // Takeoff procedure
            // if the speed is not enough to take off => return
            if (!(Speed * 3.6 >= IEnvironment.Environments.AirEnvironment.MinSpeed)) return;
            CurrentEnvironment = IEnvironment.Environments.AirEnvironment; // takeoff

            if (Speed > 0)
            {
                State = IVehicle.VehicleState.Moving;
            }
        }

        public void DecreaseSpeed(int change)
        {
            // if vehicle is on land
            if (CurrentEnvironment == IEnvironment.Environments.LandEnvironment)
            {
                // is requested speed change results in a speed greater than min land speed
                if (Speed - change <= IEnvironment.Environments.LandEnvironment.MinSpeed)
                {
                    // reduces speed
                    Speed -= change;
                }
                else
                {
                    // else sets speed to min land speed
                    Speed = IEnvironment.Environments.LandEnvironment.MinSpeed;
                }

                // does not go to the landing procedure: vehicle already on land
                return;
            }

            // Landing procedure
            // reduces speed
            Speed -= change;
            // if the speed is too high to land => return
            if ((Speed - change) / 3.6 >= IEnvironment.Environments.AirEnvironment.MinSpeed) return;
            CurrentEnvironment = IEnvironment.Environments.LandEnvironment; // landing
        }

        public override string ToString()
        {
            var vehicle = (IVehicle) this;
            var unit = CurrentEnvironment.Unit;
            return $"Type: Air Vehicle, Has Engine: {HasEngine}, " +
                   $"Current Environment: {CurrentEnvironment.Type}, " +
                   $"State: {vehicle.State}, Min Speed: {CurrentEnvironment.MinSpeed} {unit}, " +
                   $"Max Speed: {CurrentEnvironment.MaxSpeed} {unit}, Speed: {vehicle.GetConvertedSpeed(unit, Speed)} {unit}, " +
                   $"Horsepower: {Horsepower}, Fuel Type: {FuelType}";
        }
    }
}