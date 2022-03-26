#nullable enable
using System.Xml.Schema;
using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    // TODO: Finish WaterVehicle class
    public class WaterVehicle : IVehicle
    {
        public IVehicle.VehicleType Type { get; set; } = IVehicle.VehicleType.WaterVehicle;
        public IEnvironment CurrentEnvironment { get; set; } = IEnvironment.Environments.WaterEnvironment;
        public IEnvironment NativeEnvironment { get; set; } = IEnvironment.Environments.WaterEnvironment;
        public double Speed { get; set; }
        public bool HasEngine { get; set; }
        public int HorsePower { get; set; }
        public IVehicle.FuelType FuelUsed { get; set; }
        public IVehicle.State VehicleState { get; set; }
    }
}