
using System;
using ClassLibrary.Vehicles;

namespace cs_lab_Pojazdy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // TODO: Test LandVehicle more
            IVehicle car = new LandVehicle(true, 85, IVehicle.FuelType.Diesel, 4);
            car.Start();
            Console.WriteLine(car.Speed.ToString());
            car.Accelerate(5);
            Console.WriteLine(car.Speed.ToString());
            Console.WriteLine(car.ToString());
        }
    }
}