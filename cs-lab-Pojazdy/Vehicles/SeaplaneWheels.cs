using ClassLibrary.Environments;
using ClassLibrary.Vehicles;

namespace Vehicles;

public class SeaplaneWheels : MultipurposeVehicle
{
    private readonly IVehicle _vehicle;
    // A seaplane with wheels => can land on both water and land, spawns either on water or land
    public SeaplaneWheels(int horsePower, IEnvironment currentEnvironment, int displacement) : base(true, horsePower, IVehicle.FuelType.AvGas, true, 6, true, displacement, true, currentEnvironment, IEnvironment.Environments.AirEnvironment)
    {
        _vehicle = this;
    }
    
    public void Start() => _vehicle.Start();
    
    public void Stop() => _vehicle.Stop();
}