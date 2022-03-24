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
            // land vehicle spawns stationary
            State = IVehicle.VehicleState.Stationary;
            CurrentEnvironment = IEnvironment.Environments.LandEnvironment;
        }

        private double Speed { get; set; }
        private IEnvironment CurrentEnvironment { get; }
        private IVehicle.FuelType FuelType { get; }
        private int WheelCount { get; }
        public int Horsepower { get; }
        public IVehicle.VehicleState State { get; private set; }
        public bool HasEngine { get; }

        public void Start()
        {
            // if plane is already moving it cannot be started
            if (State == IVehicle.VehicleState.Moving) return;
            Speed = IEnvironment.Environments.LandEnvironment.MinSpeed;
            State = IVehicle.VehicleState.Moving;
        }

        public void Stop()
        {
            // if plane is in the air it cannot be stopped
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment) return;

            // if plane is already stopped on the ground it cannot be stopped again
            if (State != IVehicle.VehicleState.Moving) return;
            Speed = 0;
            State = IVehicle.VehicleState.Stationary;
        }

        public void IncreaseSpeed(double change)
        {
            if (Speed + change <= IEnvironment.Environments.LandEnvironment.MaxSpeed)
                Speed += change;
            else
                Speed = IEnvironment.Environments.LandEnvironment.MaxSpeed;

            if (Speed > 0) State = IVehicle.VehicleState.Moving;
        }

        public void DecreaseSpeed(double change)
        {
            if (Speed - change >= IEnvironment.Environments.LandEnvironment.MinSpeed)
                Speed -= change;
            else
                Speed = IEnvironment.Environments.LandEnvironment.MinSpeed;

            if (Speed <= 0) State = IVehicle.VehicleState.Stationary;
        }

        public override string ToString()
        {
            var vehicle = (IVehicle) this;
            var unit = CurrentEnvironment.Unit;
            return $"Type: Land Vehicle, Number of wheels: {WheelCount} " +
                   $"Horsepower: {Horsepower}, Fuel type: {FuelType} " +
                   $"Current Environment: {CurrentEnvironment.Type}, " +
                   $"State: {vehicle.State}, Min Speed: {CurrentEnvironment.MinSpeed} {unit}, " +
                   $"Max Speed: {CurrentEnvironment.MaxSpeed} {unit}, Speed: {vehicle.GetConvertedSpeed(unit, Speed)} {unit}";
        }
    }
}