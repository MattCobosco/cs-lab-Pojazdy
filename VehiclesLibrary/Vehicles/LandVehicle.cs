using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class LandVehicle : IVehicle
    {
        // Land vehicle-only properties
        private readonly int _numberOfWheels;

        public LandVehicle(bool hasEngine, int horsePower, IVehicle.FuelType fuelType, int numberOfWheels)
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

            _numberOfWheels = numberOfWheels;
        }

        // General vehicle properties
        // A land vehicle => cannot switch environments
        public IVehicle.VehicleType Type { get; set; } = IVehicle.VehicleType.LandVehicle;

        // Spawns on land
        public IEnvironment CurrentEnvironment { get; set; } = IEnvironment.Environments.LandEnvironment;

        // Native environment is land => the unit for speed and its changes will be kph
        public IEnvironment NativeEnvironment { get; set; } = IEnvironment.Environments.LandEnvironment;
        public double Speed { get; set; }
        public bool HasEngine { get; set; }
        public int HorsePower { get; set; }

        public IVehicle.FuelType FuelUsed { get; set; }

        // Spawns stationary
        public IVehicle.State VehicleState { get; set; } = IVehicle.State.Stationary;

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
                   $"Number of Wheels: {_numberOfWheels}";
        }
    }
}