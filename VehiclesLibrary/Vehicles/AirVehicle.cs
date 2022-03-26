using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public class AirVehicle : IVehicle
    {
        public IVehicle.VehicleType Type { get; set; } = IVehicle.VehicleType.AirVehicle;
        public IEnvironment CurrentEnvironment { get; set; }
        public IEnvironment NativeEnvironment { get; set; }
        public double Speed { get; set; }
        public bool HasEngine { get; set; }
        public int HorsePower { get; set; }
        public IVehicle.FuelType Fuel { get; set; }
        public IVehicle.State VehicleState { get; set; }
    }
}