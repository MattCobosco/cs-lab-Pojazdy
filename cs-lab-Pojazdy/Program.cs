using ClassLibrary.Environments;
using ClassLibrary.Vehicles;
using Vehicles;

namespace cs_lab_Pojazdy;

internal class Program
{
    private static void Main()
    {
        var vehicleList = new List<IVehicle>();

        var passengerCar = new Car(85, IVehicle.FuelType.Petrol);
        passengerCar.Start();
        passengerCar.Accelerate(139);

        var bike = new Bike();
        bike.Start();
        bike.Accelerate(15);

        var regularPlane = new RegularPlane(1000);
        regularPlane.Start();
        regularPlane.Accelerate(14);

        var glider = new Glider();
        glider.Start();
        glider.Accelerate(50);

        var motorBoat = new MotorBoat(750, 5000);
        motorBoat.Start();
        motorBoat.Accelerate(15);

        var paddleBoat = new PaddleBoat();
        paddleBoat.Start();
        paddleBoat.Accelerate(1);

        // An amphibian spawned in water
        var amphibian = new Amphibian(200, IVehicle.FuelType.Petrol, 20, IEnvironment.Environments.WaterEnvironment);
        amphibian.Start();
        amphibian.Accelerate(30);
        amphibian.Decelerate(10, null);
        //Changes amphibian environment to land
        amphibian.ChangeEnvironment(IEnvironment.Environments.LandEnvironment);

        // A seaplane with wheels spawned in water
        var seaplaneWheels = new SeaplaneWheels(250, IEnvironment.Environments.WaterEnvironment, 100);
        seaplaneWheels.Start();
        seaplaneWheels.Accelerate(10);
        // Makes the seaplane take off
        seaplaneWheels.Accelerate(20);
        // Makes the seaplane land on land
        seaplaneWheels.Decelerate(25, IEnvironment.Environments.LandEnvironment);

        // A seaplane without wheels spawned in water
        var seaplaneNoWheels = new SeaplaneNoWheels(250, 100);
        seaplaneNoWheels.Start();
        seaplaneNoWheels.Accelerate(10);
        // Makes the seaplane take off
        seaplaneNoWheels.Accelerate(10);
        // Makes the seaplane land on water
        seaplaneNoWheels.Decelerate(10, IEnvironment.Environments.WaterEnvironment);
        // Stops the seaplane
        seaplaneNoWheels.Stop();


        vehicleList.Add(passengerCar);
        vehicleList.Add(bike);
        vehicleList.Add(regularPlane);
        vehicleList.Add(glider);
        vehicleList.Add(motorBoat);
        vehicleList.Add(paddleBoat);
        vehicleList.Add(amphibian);
        vehicleList.Add(seaplaneWheels);
        vehicleList.Add(seaplaneNoWheels);

        Console.WriteLine("All vehicles:");
        foreach (var vehicle in vehicleList) Console.WriteLine($"\n{vehicle}");

        var landOnly = vehicleList.FindAll(x => x.NativeEnvironment == IEnvironment.Environments.LandEnvironment);
        Console.WriteLine("\n\nLand vehicles:");
        foreach (var vehicle in landOnly) Console.WriteLine($"\n{vehicle}");

        Console.WriteLine("\n\nAll vehicles by speed ascending:");
        foreach (var vehicle in vehicleList.OrderBy(x =>
                     IVehicle.GetConvertedSpeed(x.NativeEnvironment.Unit, IEnvironment.SpeedUnit.Kph, x.Speed)))
            Console.WriteLine($"\n{vehicle}");

        Console.WriteLine("\n\nAll vehicles currently on land environment by speed descending:");
        foreach (var vehicle in vehicleList
                     .FindAll(x => x.CurrentEnvironment == IEnvironment.Environments.LandEnvironment)
                     .OrderByDescending(x =>
                         IVehicle.GetConvertedSpeed(x.NativeEnvironment.Unit, IEnvironment.SpeedUnit.Kph, x.Speed)))
            Console.WriteLine($"\n{vehicle}");
    }
}