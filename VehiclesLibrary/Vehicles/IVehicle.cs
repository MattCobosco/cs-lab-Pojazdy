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
        FuelType Fuel { get; set; }
        State VehicleState { get; set; }

        void Stop()
        {
            Speed = 0;
            VehicleState = State.Stationary;
        }

        void Start()
        {
            Speed = GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, CurrentEnvironment.MinSpeed);
            VehicleState = State.Moving;
        }

        void Accelerate(double speedInNativeUnit)
        {
            Speed += Math.Min(
                GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, CurrentEnvironment.MaxSpeed),
                GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, speedInNativeUnit));

            if (Speed > 0)
                VehicleState = State.Moving;
        }

        void Decelerate(double speedInNativeUnit)
        {
            Speed -= Math.Max(
                GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, CurrentEnvironment.MinSpeed),
                GetConvertedSpeed(CurrentEnvironment.Unit, NativeEnvironment.Unit, speedInNativeUnit));
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