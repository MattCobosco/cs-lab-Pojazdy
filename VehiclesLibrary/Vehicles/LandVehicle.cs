using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class LandVehicle : IVehicle
    {
        // LandVehicle spawns on the ground
        private readonly IEnvironment CurrentEnvironment = IEnvironment.Environments.LandEnvironment;

        public LandVehicle(int horsepower, bool hasEngine, IVehicle.FuelType fuelType, int wheelCount)
        {
            Horsepower = horsepower;
            HasEngine = hasEngine;
            FuelType = fuelType;
            WheelCount = wheelCount;
        }

        private double Speed { get; set; }
        private IVehicle.FuelType FuelType { get; }
        private int WheelCount { get; }

        public int Horsepower { get; }

        // LandVehicle Spawns as stationary
        public IVehicle.VehicleState State { get; private set; } = IVehicle.VehicleState.Stationary;
        public bool HasEngine { get; }

        public void Start()
        {
            Speed = CurrentEnvironment.MinSpeed;
            State = IVehicle.VehicleState.Moving;
        }

        public void Stop()
        {
            Speed = 0;
            State = IVehicle.VehicleState.Stationary;
        }

        public void IncreaseSpeed(double changeInKps)
        {
            if (Speed + changeInKps <= IEnvironment.Environments.LandEnvironment.MaxSpeed)
                Speed += changeInKps;
            else
                Speed = IEnvironment.Environments.LandEnvironment.MaxSpeed;

            if (Speed > 0) State = IVehicle.VehicleState.Moving;
        }

        public void DecreaseSpeed(double changeInKps)
        {
            if (Speed - changeInKps >= IEnvironment.Environments.LandEnvironment.MinSpeed)
                Speed -= changeInKps;
            else
                Speed = IEnvironment.Environments.LandEnvironment.MinSpeed;

            if (Speed <= 0) State = IVehicle.VehicleState.Stationary;
        }

        public override string ToString()
        {
            var vehicle = (IVehicle) this;
            var currentUnit = CurrentEnvironment.Unit;
            return $"Type: Land Vehicle, Number of wheels: {WheelCount} " +
                   $"Horsepower: {Horsepower}, Fuel type: {FuelType} " +
                   $"Current Environment: {CurrentEnvironment.Type}, " +
                   $"State: {vehicle.State}, Min Speed: {CurrentEnvironment.MinSpeed} {currentUnit}, " +
                   $"Max Speed: {CurrentEnvironment.MaxSpeed} {currentUnit}, Speed: {Speed} {currentUnit}";
        }
    }
}