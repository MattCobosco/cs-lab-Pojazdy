using ClassLibrary.Vehicles;

namespace Vehicles;

public class RegularPlane : AirVehicle
{
    private readonly IVehicle _vehicle;

    public RegularPlane(int horsePower) : base(true, horsePower, IVehicle.FuelType.AvGas)
    {
        _vehicle = this;
    }

    public void Start() => _vehicle.Start();
    
    public void Stop() => _vehicle.Stop();
}