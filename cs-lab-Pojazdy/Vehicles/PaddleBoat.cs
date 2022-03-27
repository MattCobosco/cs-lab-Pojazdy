using ClassLibrary.Vehicles;

namespace Vehicles;

public class PaddleBoat : WaterVehicle
{
    private readonly IVehicle _vehicle;
    
    public PaddleBoat() : base(false, 0, 150)
    {
        _vehicle = this;
    }
    
    public void Start() => _vehicle.Start();
    
    public void Stop() => _vehicle.Stop();
    
    public void Accelerate(double speedChange) => _vehicle.Accelerate(speedChange);

    public void Decelerate(double speedChange) => _vehicle.Decelerate(speedChange);
}