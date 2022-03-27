using ClassLibrary.Environments;
using ClassLibrary.Vehicles;

namespace Vehicles;

public class SeaplaneNoWheels : MultipurposeVehicle
{
    private readonly IVehicle _vehicle;
    // A seaplane without wheels => can land on water, spawns in water on default
    public SeaplaneNoWheels(int horsePower, int displacement) : base(true, horsePower, IVehicle.FuelType.AvGas, false, 0, true, displacement, true, IEnvironment.Environments.WaterEnvironment, IEnvironment.Environments.AirEnvironment)
    {
        _vehicle = this;
    }
    
    public void Start() => _vehicle.Start();
    
    public void Stop() => _vehicle.Stop();
}