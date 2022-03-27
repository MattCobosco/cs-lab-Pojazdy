using ClassLibrary.Vehicles;

namespace Vehicles;

public class MotorBoat : WaterVehicle
{
    private readonly IVehicle _vehicle;
    
    public MotorBoat(int horsePower, int displacement) : base(true, horsePower, displacement)
    {
        _vehicle = this;
    }

    public void Start() => _vehicle.Start();
    
    public void Stop() => _vehicle.Stop();
    
    public void Accelerate(double speedChange) => _vehicle.Accelerate(speedChange);

    public void Decelerate(double speedChange) => _vehicle.Decelerate(speedChange);
}