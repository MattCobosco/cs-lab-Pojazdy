using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class LandVehicle : IVehicle
    {
        // General Vehicle properties
        // A land vehicle => can't switch environments
        public IVehicle.VehicleType Type { get; set; } = IVehicle.VehicleType.LandVehicle;
        public IEnvironment CurrentEnvironment { get; set; } = IEnvironment.Environments.LandEnvironment;
        public IEnvironment NativeEnvironment { get; set; } = IEnvironment.Environments.LandEnvironment;
        public double Speed { get; set; }
        public bool HasEngine { get; set; }
        public int HorsePower { get; set; }
        public IVehicle.FuelType Fuel { get; set; }
        public IVehicle.State VehicleState { get; set; }
        
        // LandVehicle-only properties
        private readonly int _numberOfWheels;
        
        public LandVehicle(bool hasEngine, int horsePower, IVehicle.FuelType fuel, int numberOfWheels)
        {
            HasEngine = hasEngine;
            if (HasEngine)
            {
                HorsePower = horsePower;
                Fuel = fuel;
            }
            else
            {
                // if no engine => horsepower is 0, fuel type is none
                HorsePower = 0;
                Fuel = IVehicle.FuelType.None;
            }
            _numberOfWheels = numberOfWheels;
        }
        
        public override string ToString()
        {
            var vehicle = (IVehicle) this;
            var nativeUnit = vehicle.NativeEnvironment.Unit;
            var currentUnit = vehicle.CurrentEnvironment.Unit;

            return $"Data Type: {GetType().Name}, " +
                   $"Vehicle Type: {vehicle.Type}, " +
                   $"Current Environment: {CurrentEnvironment.Type}, " +
                   $"Current State: {vehicle.VehicleState}, " +
                   $"Environment Min Speed: {CurrentEnvironment.MinSpeed} {currentUnit}, " +
                   $"Environment Max Speed: {CurrentEnvironment.MaxSpeed} {currentUnit}, " +
                   $"Current Speed: {IVehicle.GetConvertedSpeed(nativeUnit, currentUnit, vehicle.Speed)} {nativeUnit}, " +
                   $"ADDITIONAL: " +
                   $"HasEngine: {vehicle.HasEngine}, " +
                   $"Number of Wheels: {_numberOfWheels}";
        }
    }
}