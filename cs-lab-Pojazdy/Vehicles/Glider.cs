using ClassLibrary.Vehicles;

namespace Vehicles;

public class Glider : AirVehicle
{
    private readonly IVehicle _vehicle;
    
    public Glider() : base(false, 0, IVehicle.FuelType.None)
    {
        _vehicle = this;
    }

    public void Start() => _vehicle.Start();
    
    public void Stop() => _vehicle.Stop();
    
}