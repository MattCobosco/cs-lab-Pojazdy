using ClassLibrary.Vehicles;

namespace Vehicles;

public class Car : LandVehicle, IVehicle
{
    public Car(bool hasEngine, int horsePower, IVehicle.FuelType fuelType, int numberOfWheels) : base(hasEngine, horsePower, fuelType, numberOfWheels)
    {
    }
}