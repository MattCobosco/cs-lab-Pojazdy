#nullable enable
using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class WaterVehicle : IVehicle
    {
        public WaterVehicle(int horsepower, int displacement, bool? amphibious, int? wheelCount, IEnvironment? currentEnvironment)
        {
            Horsepower = horsepower;
            // always has engine
            HasEngine = true;
            // always powered by diesel
            _fuelType = IVehicle.FuelType.Diesel;
            _displacement = displacement;
            // spawns stationary
            State = IVehicle.VehicleState.Stationary;
            // can be amphibious
            // if amphibious is null then thr vehicle is not amphibious
            _amphibious = amphibious ?? false;
            // can have wheels if amphibious 
            _wheelCount = wheelCount ?? 0;
            // if currentEnvironment is null then vehicle spawns in water
            CurrentEnvironment = currentEnvironment ?? IEnvironment.Environments.WaterEnvironment;
        }

        private int Speed { get; set; }
        public IVehicle.VehicleState State { get; set; }
        public bool HasEngine { get; }
        public int Horsepower { get; }
        private IVehicle.FuelType _fuelType;
        private int _displacement;
        private IEnvironment CurrentEnvironment { get; }
        private bool _amphibious;
        private int _wheelCount;

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

        public void IncreaseSpeed(int change)
        {
            if (Speed + change <= CurrentEnvironment.MaxSpeed)
            {
                Speed += change;
            }
            else
            {
                Speed = CurrentEnvironment.MaxSpeed;
            }
        }

        public void DecreaseSpeed(int change)
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
        
        // TODO: GoOnLand, GoOnWater - check speed differences between the environments
        public void GoOnWater()
        {
            
        }

        public void GoOnLand()
        {
            
        }

        public override string ToString()
        {
            var vehicle = (IVehicle) this;
            var unit = CurrentEnvironment.Unit;
            // TODO: Smarter strings
            if (_amphibious)
            {
                return $"Type: Amphibious Vehicle, Number of wheels: {_wheelCount}" +
                       $"Displacement:{_displacement}, Horsepower: {Horsepower}" +
                       $"Has Engine: {HasEngine}, Fuel Type: {_fuelType} " +
                       $"Current Environment: {CurrentEnvironment.Type} " +
                       $"State: {vehicle.State}, Min Speed: {CurrentEnvironment.MinSpeed} " +
                       $"Max Speed: {CurrentEnvironment.MaxSpeed} {unit}, Speed: {vehicle.GetConvertedSpeed(unit, Speed)} {unit}";
            }
            else
            {
                return $"Type: Water Vehicle, Displacement {_displacement}, " +
                       $"Horsepower: {Horsepower}, Has Engine: {HasEngine}, " +
                       $"Fuel Type: {_fuelType}, Current Environment {CurrentEnvironment.Type}, " +
                       $"State: {vehicle.State}, Min Speed: {CurrentEnvironment.MinSpeed} {unit}," +
                       $"Max Speed: {CurrentEnvironment.MaxSpeed} {unit}," +
                       $"Speed: {vehicle.GetConvertedSpeed(unit, Speed)}";
            }
        }
    }
}