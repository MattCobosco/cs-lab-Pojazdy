#nullable enable
using System;
using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class MultipurposeVehicle : IVehicle
    {
        // Multipurpose vehicle-specific properties
        private readonly int _numberOfWheels;
        private readonly int _displacement;
        private readonly bool _canOperateOnLand;
        private readonly bool _canOperateInAir;

        private readonly bool _canOperateOnWater;

        // General vehicle properties
        // Multi-purpose vehicle => can switch between environments selected on creation
        public IVehicle.VehicleType Type { get; set; } = IVehicle.VehicleType.MultipurposeVehicle;

        // No default current environment => spawns on land or water depending on environment selected on creation
        public IEnvironment CurrentEnvironment { get; set; }

        // No default native environment => no default unit for speed and speed changes
        public IEnvironment NativeEnvironment { get; set; }
        public double Speed { get; set; }
        public bool HasEngine { get; set; }
        public int HorsePower { get; set; }

        public IVehicle.FuelType FuelUsed { get; set; }

        // Spawns stationary
        public IVehicle.State VehicleState { get; set; } = IVehicle.State.Stationary;

        public MultipurposeVehicle(bool hasEngine, int horsePower, IVehicle.FuelType fuelType,
            bool canOperateOnLand, int numberOfWheels, bool canOperateOnWater, int displacement, bool canOperateInAir,
            IEnvironment currentEnvironment, IEnvironment nativeEnvironment)
        {
            HasEngine = hasEngine;
            if (HasEngine)
            {
                HorsePower = horsePower;
                FuelUsed = fuelType;
            }
            else
            {
                HorsePower = 0;
                FuelUsed = IVehicle.FuelType.None;
            }

            _canOperateOnLand = canOperateOnLand;
            _numberOfWheels = numberOfWheels;
            _canOperateOnWater = canOperateOnWater;
            // If can't operate on water, the displacement can be omitted
            _displacement = _canOperateOnWater ? displacement : 0;
            _canOperateInAir = canOperateInAir;
            CurrentEnvironment = currentEnvironment;
            NativeEnvironment = nativeEnvironment;

            // Exception handling for invalid environment input on creation
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment)
                throw new ArgumentException("Multipurpose vehicle cannot be created in air.");
            if (!_canOperateOnLand && (CurrentEnvironment == IEnvironment.Environments.LandEnvironment ||
                                       NativeEnvironment == IEnvironment.Environments.LandEnvironment))
                throw new Exception(
                    "This multipurpose vehicle cannot operate on land. Change its parameters and try again.");
            if (!_canOperateOnWater && (CurrentEnvironment == IEnvironment.Environments.WaterEnvironment ||
                                        NativeEnvironment == IEnvironment.Environments.WaterEnvironment))
                throw new Exception(
                    "This multipurpose vehicle cannot operate on water. Change its parameters and try again.");
            if (!_canOperateInAir && NativeEnvironment == IEnvironment.Environments.AirEnvironment)
                throw new Exception(
                    "This multipurpose vehicle cannot operate in air. Change its parameters and try again.");
        }

        public void ChangeEnvironment(IEnvironment toEnvironment)
        {
            // A stationary vehicle cannot change environment
            if (VehicleState == IVehicle.State.Stationary) return;
            // Exception handling for invalid function usage
            if (toEnvironment == IEnvironment.Environments.AirEnvironment ||
                CurrentEnvironment == IEnvironment.Environments.AirEnvironment)
                throw new Exception(
                    "Cannot take off/land using change environment method. Use Accelerate/Decelerate methods instead.");
            // Change environment
            CurrentEnvironment = toEnvironment;
            // Check speed limits
            if (Speed > IVehicle.GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit,
                    CurrentEnvironment.MaxSpeed))
                Speed = IVehicle.GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, Speed);
            else if (Speed < IVehicle.GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit,
                         CurrentEnvironment.MinSpeed))
                Speed = IVehicle.GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, Speed);
        }

        public void Accelerate(double changeInNativeUnit)
        {
            // A stationary vehicle cannot accelerate
            if (VehicleState == IVehicle.State.Stationary) return;
            // Takeoff is available only for vehicles that can operate in air
            if (_canOperateInAir)
            {
                // Increase speed but not above the maximum speed of air environment converted to native speed unit
                Speed += IVehicle.GetConvertedSpeed(IEnvironment.Environments.AirEnvironment.Unit,
                    NativeEnvironment.Unit,
                    Math.Min(IEnvironment.Environments.AirEnvironment.MaxSpeed, Speed + changeInNativeUnit));
                // If vehicle is already in air don't check takeoff requirements
                if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment) return;
                // Takeoff if takeoff speed is reached
                if (Speed >= IEnvironment.Environments.AirEnvironment.MinSpeed)
                    CurrentEnvironment = IEnvironment.Environments.AirEnvironment;
            }
            // No takeoff, accelerate within max speed of current environment
            else
            {
                Speed = Math.Min(
                    IVehicle.GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit,
                        CurrentEnvironment.MaxSpeed), Speed + changeInNativeUnit);
            }
        }

        public void Decelerate(double changeInNativeUnit, IEnvironment? landingEnvironment)
        {
            // A stationary vehicle cannot decelerate
            if (VehicleState == IVehicle.State.Stationary) return;
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment && landingEnvironment != null)
            {
                // If vehicle is in air and landing environment is not null, it can try to land
                Speed = Math.Max(
                    IVehicle.GetConvertedSpeed(landingEnvironment.Unit, NativeEnvironment.Unit,
                        landingEnvironment.MinSpeed), Speed - changeInNativeUnit);
                // Land if landing speed is reached
                if (Speed <= IEnvironment.Environments.AirEnvironment.MinSpeed) CurrentEnvironment = landingEnvironment;
            }
            else
            {
                // If vehicle is not in air then it can only decelerate within its current environment
                Speed = Math.Max(
                    IVehicle.GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit,
                        CurrentEnvironment.MinSpeed), Speed - changeInNativeUnit);
            }
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
                   $"Type of Fuel: {vehicle.FuelUsed}, " +
                   $"Number of Wheels: {_numberOfWheels}, " +
                   $"Displacement: {_displacement}, " +
                   $"Can operate on land: {_canOperateOnLand}, " +
                   $"Can operate in air: {_canOperateInAir}, " +
                   $"Can operate on water: {_canOperateOnWater}";
        }
    }
}