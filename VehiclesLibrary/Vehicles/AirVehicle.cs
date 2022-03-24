using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class AirVehicle : IVehicle
    {
        public AirVehicle(int horsepower, bool hasEngine, IVehicle.FuelType fuelType)
        {
            Horsepower = horsepower;
            HasEngine = hasEngine;
            FuelType = fuelType;
            // air vehicle spawns on the ground
            CurrentEnvironment = IEnvironment.Environments.LandEnvironment;
            // air vehicle spawns as stationary
            State = IVehicle.VehicleState.Stationary;
        }

        private double Speed { get; set; }
        private static IEnvironment CurrentEnvironment { get; set; }
        private IVehicle.FuelType FuelType { get; }
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
        // TODO: Fix AirVehicle ignoring min and max speed boundaries while changing environments
        public void IncreaseSpeed(double change)
        {
            // if vehicle is in air => check max speed limit and increase speed
            if (CurrentEnvironment == IEnvironment.Environments.AirEnvironment)
            {
                if ((Speed + change) * 3.6 <= IEnvironment.Environments.AirEnvironment.MaxSpeed) Speed += change;
                else Speed = IEnvironment.Environments.AirEnvironment.MaxSpeed; 
                return;
            }

            Speed += change;
            // Takeoff procedure
            // if the speed is not enough to take off => return
            if (!(Speed * 3.6 >= IEnvironment.Environments.AirEnvironment.MinSpeed)) return;
            CurrentEnvironment = IEnvironment.Environments.AirEnvironment; // takeoff

            if (Speed > 0) State = IVehicle.VehicleState.Moving;
        }

        public void DecreaseSpeed(double change)
        {
            // if vehicle is on land
            if (CurrentEnvironment == IEnvironment.Environments.LandEnvironment)
            {
                // is requested speed change results in a speed greater than min land speed
                if (Speed - change <= IEnvironment.Environments.LandEnvironment.MinSpeed)
                    // reduces speed
                    Speed -= change;
                else
                    // else sets speed to min land speed
                    Speed = IEnvironment.Environments.LandEnvironment.MinSpeed;

                // does not go to the landing procedure: vehicle already on land
                return;
            }

            // Landing procedure
            // reduces speed
            Speed -= change;
            // if the speed is too high to land => return
            if ((Speed - change) / 3.6 >= IEnvironment.Environments.AirEnvironment.MinSpeed) return;
            CurrentEnvironment = IEnvironment.Environments.LandEnvironment; // landing
        }

        public override string ToString()
        {
            var vehicle = (IVehicle) this;
            var unit = CurrentEnvironment.Unit;
            return $"Type: Air Vehicle, Has Engine: {HasEngine}, " +
                   $"Current Environment: {CurrentEnvironment.Type}, " +
                   $"State: {vehicle.State}, Min Speed: {CurrentEnvironment.MinSpeed} {unit}, " +
                   $"Max Speed: {CurrentEnvironment.MaxSpeed} {unit}, Speed: {(int) vehicle.GetConvertedSpeed(unit, Speed)} {unit}, " +
                   $"Horsepower: {Horsepower}, Fuel Type: {FuelType}";
        }
    }
}