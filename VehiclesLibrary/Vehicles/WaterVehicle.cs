#nullable enable
using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class WaterVehicle : IVehicle
    {
        // TODO: Test WaterVehicle in console and fix potential issues
        public WaterVehicle(int horsepower, int displacement, bool hasEngine, bool? amphibious, int? wheelCount, IEnvironment? currentEnvironment)
        {
            Horsepower = horsepower;
            HasEngine = hasEngine;
            // engine is always a diesel engine
            if(HasEngine)
                _fuelType = IVehicle.FuelType.Diesel;
            _displacement = displacement;
            // spawns stationary
            State = IVehicle.VehicleState.Stationary;
            // can be amphibious
            // if amphibious is null then the vehicle is not amphibious
            _amphibious = amphibious ?? false;
            // can have wheels if amphibious 
            _wheelCount = wheelCount ?? 0;
            // if currentEnvironment is null then vehicle spawns in water
            CurrentEnvironment = currentEnvironment ?? IEnvironment.Environments.WaterEnvironment;
        }

        private double Speed { get; set; }
        public IVehicle.VehicleState State { get; private set; }
        public bool HasEngine { get; }
        public int Horsepower { get; }
        private readonly IVehicle.FuelType _fuelType;
        private readonly int _displacement;
        private IEnvironment CurrentEnvironment { get; set; }
        private readonly bool _amphibious;
        private readonly int _wheelCount;

        public void Start()
        {
            if (State != IVehicle.VehicleState.Moving) return;
            Speed = CurrentEnvironment.MinSpeed;
            State = IVehicle.VehicleState.Moving;
        }

        public void Stop()
        {
            if (State != IVehicle.VehicleState.Stationary) return;
            Speed = 0;
            State = IVehicle.VehicleState.Stationary;
        }

        public void IncreaseSpeed(double change)
        {
            if (Speed + change <= CurrentEnvironment.MaxSpeed)
            {
                Speed += change;
            }
            else
            {
                Speed = CurrentEnvironment.MaxSpeed;
            }

            if (Speed > 0)
            {
                State = IVehicle.VehicleState.Moving;
            }
        }

        public void DecreaseSpeed(double change)
        {
            if (Speed - change >= CurrentEnvironment.MinSpeed)
            {
                Speed -= change;
            }
            else
            {
                Speed = CurrentEnvironment.MinSpeed;
            }
        }
        
        // Change between Water and Land Environments
        public void ChangeEnvironment()
        {
            // non-amphibious vehicle cannot switch environment
            if (!_amphibious) return;
            if (IVehicle.CurrentEnvironment == IEnvironment.Environments.LandEnvironment)
            {
                CurrentEnvironment = IEnvironment.Environments.WaterEnvironment;
                Speed *= 0.54;
            }
            else
            {
                CurrentEnvironment = IEnvironment.Environments.LandEnvironment;
            }
        }


        public override string ToString()
        {
            var vehicle = (IVehicle) this;
            var unit = CurrentEnvironment.Unit;
            var firstPart = _amphibious ? $"Type: Amphibious Vehicle, Number of wheels: {_wheelCount}" : "Type: Water Vehicle,";
            
            return $"{firstPart} " +
                   $"Displacement {_displacement}, Horsepower: {Horsepower}, " +
                   $"Has Engine: {HasEngine}, Fuel Type: {_fuelType}, " +
                   $"Current Environment {CurrentEnvironment.Type}, Min Speed: {CurrentEnvironment.MinSpeed} {unit}, " +
                   $"Max Speed: {CurrentEnvironment.MaxSpeed} {unit}, Speed: {vehicle.GetConvertedSpeed(unit, Speed)}";
        }
    }
}