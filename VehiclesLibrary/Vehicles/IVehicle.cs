using System;
using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public interface IVehicle
    {
        enum FuelType
        {
            Petrol,
            Diesel,
            Electric,
            Hybrid,
            Avgas,
            Other,
            None
        }

        enum State
        {
            Moving,
            Stationary
        }

        enum VehicleType
        {
            LandVehicle,
            WaterVehicle,
            AirVehicle,
            MultipurposeVehicle
        }
        
        VehicleType Type { get; set; }
        IEnvironment CurrentEnvironment { get; set; }
        IEnvironment NativeEnvironment { get; set; }
        double Speed { get; set; }
        bool HasEngine { get; set; }
        int HorsePower { get; set; }
        FuelType FuelUsed { get; set; }
        State VehicleState { get; set; }
        
        void Start()
        {
            // A moving vehicle cannot be started
            if (VehicleState == State.Moving) return;
            // A started vehicle reaches the current environment minimum speed
            Speed = GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, CurrentEnvironment.MinSpeed);
            VehicleState = State.Moving;
        }
        
        void Stop()
        {
            // Vehicle cannot be stopped mid-air
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment) return;
            // A stationary vehicle cannot be stopped
            if (VehicleState == State.Stationary) return;
            Speed = 0;
            VehicleState = State.Stationary;
        }

        void Accelerate(double changeInNativeUnit)
        {
            // A stationary vehicle cannot accelerate
            if (VehicleState == State.Stationary) return;
            Speed = Math.Min(
                GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, CurrentEnvironment.MaxSpeed),
                GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, Speed + changeInNativeUnit));
        }

        void Decelerate(double changeInNativeUnit)
        {
            // A stationary vehicle cannot decelerate
            if (VehicleState == State.Stationary) return;
            Speed = Math.Max(
                GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, CurrentEnvironment.MinSpeed),
                GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, Speed - changeInNativeUnit));
        }

        static double GetConvertedSpeed(IEnvironment.SpeedUnit fromUnit, IEnvironment.SpeedUnit toUnit, double speed)
        {
            return fromUnit switch
            {
                IEnvironment.SpeedUnit.Kph when toUnit == IEnvironment.SpeedUnit.Mps => speed / 3.6,
                IEnvironment.SpeedUnit.Mps when toUnit == IEnvironment.SpeedUnit.Kph => speed * 3.6,
                IEnvironment.SpeedUnit.Kph when toUnit == IEnvironment.SpeedUnit.Knots => speed * 1.852,
                IEnvironment.SpeedUnit.Knots when toUnit == IEnvironment.SpeedUnit.Kph => speed / 1.852,
                _ => speed
            };
        }
    }
}