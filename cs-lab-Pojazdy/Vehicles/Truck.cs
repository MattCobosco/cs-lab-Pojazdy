using ClassLibrary.Vehicles;

namespace Vehicles;

public class Truck : LandVehicle
{
    private readonly IVehicle _vehicle;
    
    public Truck(int horsePower) : base(true, horsePower, IVehicle.FuelType.Diesel, 18)
    {
        _vehicle = this;
    }
    
    public void Start() => _vehicle.Start();

    public void Stop() => _vehicle.Stop();
    
    public void Accelerate(double speedChange) => _vehicle.Accelerate(speedChange);
    
    public void Decelerate(double speedChange) => _vehicle.Decelerate(speedChange);
}