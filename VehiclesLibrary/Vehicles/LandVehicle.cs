using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class LandVehicle : IVehicle
    {
        public LandVehicle(int horsepower, bool hasEngine, IVehicle.FuelType fuelType, int wheelCount)
        {
            Horsepower = horsepower;
            HasEngine = hasEngine;
            FuelType = fuelType;
            WheelCount = wheelCount;
        }

        public int Speed { get; set; } = 0;
        public IEnvironment CurrentEnvironment { get; } = new LandEnvironment();
        public IVehicle.FuelType FuelType { get; }
        public int WheelCount { get; }
        public int Horsepower { get; }
        public bool HasEngine { get; set; }

        public override string ToString()
        {
            var vehicle = (IVehicle) this;
            var unit = CurrentEnvironment.Unit;
            return $"Type: Land Vehicle, Current Environment: {CurrentEnvironment.Type}," +
                   $"State:{vehicle.State}, Min Speed:{CurrentEnvironment.MinSpeed}{unit}," +
                   $"Max Speed:{CurrentEnvironment.MinSpeed}{unit}, Speed:{Speed}{unit}, " +
                   $"Horse Power:{Horsepower}, Fuel type: {FuelType}, Wheel Count: {WheelCount}";
        }
    }
}