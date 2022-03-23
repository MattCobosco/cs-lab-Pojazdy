using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class AirVehicle : IVehicle
    {
        public static IEnvironment LandEnvironment = new LandEnvironment();
        public static IEnvironment AirEnvironment = new AirEnvironment();

        public AirVehicle(int horsepower, bool hasEngine, IVehicle.FuelType fuelType)
        {
            Horsepower = horsepower;
            HasEngine = hasEngine;
            FuelType = fuelType;
        }

        public int Speed { get; set; }
        public static IEnvironment CurrentEnvironment { get; private set; } = LandEnvironment;
        public IVehicle.FuelType FuelType { get; }
        public int Horsepower { get; }
        public bool HasEngine { get; set; }

        public void IncreaseSpeed(int change)
        {
            if (Speed + change <= CurrentEnvironment.MaxSpeed) Speed += change;

            if (CurrentEnvironment.Type != IEnvironment.EnvironmentType.Land) return;
            if (Speed >= AirEnvironment.MinSpeed)
                CurrentEnvironment = AirEnvironment; // takeoff
        }

        public void DecreaseSpeed(int change)
        {
        }

        public override string ToString()
        {
            var vehicle = (IVehicle)this;
            var unit = CurrentEnvironment.Unit;
            return $"Type: Air Vehicle, Current Environment: {CurrentEnvironment.Type}," +
                   $"State: {vehicle.State}, Min Speed:{CurrentEnvironment.MinSpeed} {unit}," +
                   $"Max Speed: {CurrentEnvironment.MaxSpeed} {unit}, Speed: {Speed} {unit}," +
                   $"Horse Power: {Horsepower}, Fuel type: {FuelType}";
        }
    }
}