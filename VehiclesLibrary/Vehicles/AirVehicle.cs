using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class AirVehicle : IVehicle
    {
        // its default environment is AirEnvironment
        private static readonly IEnvironment NativeEnvironment = IEnvironment.Environments.AirEnvironment;

        public AirVehicle(int horsepower, bool hasEngine, IVehicle.FuelType fuelType)
        {
            Horsepower = horsepower;
            HasEngine = hasEngine;
            FuelType = fuelType;
        }

        // native speed unit for AirVehicle is meters per second, all requested speed changes are made in mps
        private double Speed { get; set; }

        // AirVehicle spawns on the ground
        private static IEnvironment CurrentEnvironment { get; set; } = IEnvironment.Environments.LandEnvironment;
        private IVehicle.FuelType FuelType { get; }

        public int Horsepower { get; }

        // AirVehicle spawns as stationary
        public IVehicle.VehicleState State { get; private set; } = IVehicle.VehicleState.Stationary;
        public bool HasEngine { get; }

        public void Start()
        {
            var vehicle = (IVehicle) this;
            // if plane is already moving it cannot be started
            if (State == IVehicle.VehicleState.Moving) return;
            Speed = vehicle.GetConvertedSpeed(IEnvironment.SpeedUnit.Kph, IEnvironment.SpeedUnit.Mps,
                CurrentEnvironment.MinSpeed);
            State = IVehicle.VehicleState.Moving;
        }

        public void Stop()
        {
            // if plane is in the air it cannot be stopped
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment) return;
            Speed = 0;
            State = IVehicle.VehicleState.Stationary;
        }

        public void IncreaseSpeed(double changeInMps)
        {
            // increasing speed in air
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment)
            {
                // if the requested speed change is within the max speed limit of the AirEnvironment then it's implemented
                if (Speed + changeInMps <= IEnvironment.Environments.AirEnvironment.MaxSpeed)
                {
                    Speed += changeInMps;
                    return;
                }

                // if it exceeds the max limit then the max is the new speed
                Speed = CurrentEnvironment.MaxSpeed;
                return;
            }

            // increasing speed on land
            Speed += changeInMps;

            if (Speed > IEnvironment.Environments.AirEnvironment.MinSpeed)
                //takeoff procedure
                CurrentEnvironment = IEnvironment.Environments.AirEnvironment;
            // check if requested speed does not exceed the max speed of AirEnvironment
            if (Speed > CurrentEnvironment.MaxSpeed)
                //if so, reduce it to max speed
                Speed = CurrentEnvironment.MaxSpeed;
            // for situation when air vehicle skips Start() and immediately uses IncreaseSpeed() method
            if (Speed > 0)
                State = IVehicle.VehicleState.Moving;
        }

        public void DecreaseSpeed(double changeInMps)
        {
            var vehicle = (IVehicle) this;
            // if vehicle is on land
            if (CurrentEnvironment == IEnvironment.Environments.LandEnvironment)
            {
                // is requested speed change results in a speed greater than min land speed
                if (Speed - changeInMps <= CurrentEnvironment.MinSpeed)
                    // reduces speed
                    Speed -= changeInMps;
                else
                    // else sets speed to min land speed
                    Speed = IEnvironment.Environments.LandEnvironment.MinSpeed;
                // does not go to the landing procedure: vehicle already on land
                return;
            }

            // Landing procedure
            // reduces speed
            Speed -= changeInMps;
            // if the speed is too high to land, return
            if (Speed - changeInMps >= IEnvironment.Environments.AirEnvironment.MinSpeed)
                return;
            CurrentEnvironment = IEnvironment.Environments.LandEnvironment; // landing
            if (Speed < CurrentEnvironment.MinSpeed) // check speed: can't be less than land min speed
                Speed = vehicle.GetConvertedSpeed(IEnvironment.SpeedUnit.Kph, IEnvironment.SpeedUnit.Mps,
                    CurrentEnvironment.MinSpeed);
        }

        // current speed is shown in the current environment speed unit
        public override string ToString()
        {
            var vehicle = (IVehicle) this;
            var currentUnit = CurrentEnvironment.Unit;
            var nativeUnit = NativeEnvironment.Unit;
            return $"Type: Air Vehicle, Has Engine: {HasEngine}, " +
                   $"Current Environment: {CurrentEnvironment.Type}, " +
                   $"State: {vehicle.State}, Min Speed: {CurrentEnvironment.MinSpeed} {currentUnit}, " +
                   $"Max Speed: {CurrentEnvironment.MaxSpeed} {currentUnit}, " +
                   $"Speed: {vehicle.GetConvertedSpeed(nativeUnit, currentUnit, Speed)} {currentUnit}, " +
                   $"Horsepower: {Horsepower}, Fuel Type: {FuelType}";
        }
    }
}