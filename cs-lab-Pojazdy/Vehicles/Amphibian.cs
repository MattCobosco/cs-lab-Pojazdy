using ClassLibrary.Environments;
using ClassLibrary.Vehicles;

namespace Vehicles;

public class Amphibian : MultipurposeVehicle
{
    private readonly IVehicle _vehicle;
    
    public Amphibian(int horsePower, IVehicle.FuelType fuelType, int displacement, IEnvironment currentEnvironment) : base(true, horsePower, fuelType, true, 0, true, displacement, false, currentEnvironment, IEnvironment.Environments.WaterEnvironment)
    {
        _vehicle = this;
    }

    public void Start() => _vehicle.Start();
    
    public void Stop() => _vehicle.Stop();
}