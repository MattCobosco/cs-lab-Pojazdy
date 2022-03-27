using ClassLibrary.Vehicles;

namespace Vehicles;

public class Bike : LandVehicle
{
    private readonly IVehicle _vehicle;
    
    public Bike() : base(false, 0, IVehicle.FuelType.None, 2)
    {
        _vehicle = this;
    }
    
    public void Start() => _vehicle.Start();

    public void Stop() => _vehicle.Stop();

    public void Accelerate(double speedChange) => _vehicle.Accelerate(speedChange);
    
    public void Decelerate(double speedChange) => _vehicle.Decelerate(speedChange);
}