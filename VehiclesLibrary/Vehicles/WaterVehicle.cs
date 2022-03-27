#nullable enable
using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    // TODO: Finish WaterVehicle class
    public class WaterVehicle : IVehicle
    {
        // Water vehicle-specific properties
        private readonly int _displacement;

        // General vehicle properties
        // A water vehicle => cannot switch environments
        public IVehicle.VehicleType Type { get; set; } = IVehicle.VehicleType.WaterVehicle;
        // Spawns on water
        public IEnvironment CurrentEnvironment { get; set; } = IEnvironment.Environments.WaterEnvironment;
        // Native environment is water => the unit for speed and speed changes is knots
        public IEnvironment NativeEnvironment { get; set; } = IEnvironment.Environments.WaterEnvironment;
        public double Speed { get; set; }
        public bool HasEngine { get; set; }
        public int HorsePower { get; set; }
        public IVehicle.FuelType FuelUsed { get; set; }
        // Spawns stationary
        public IVehicle.State VehicleState { get; set; } = IVehicle.State.Stationary;

        public WaterVehicle(bool hasEngine, int horsePower, int displacement)
        {
            HasEngine = hasEngine;
            if (hasEngine)
            {
                HorsePower = horsePower;
                FuelUsed = IVehicle.FuelType.Diesel;
            }
            else
            {
                HorsePower = 0;
                FuelUsed = IVehicle.FuelType.None;
            }

            _displacement = displacement;
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
                   $"Displacement: {_displacement}";
        }
    }
}