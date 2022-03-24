using System;
using ClassLibrary.Vehicles;

namespace cs_lab_Pojazdy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /* AirVehicle
             */ 
            var plane = new AirVehicle(200, true, IVehicle.FuelType.Petrol);
            Console.WriteLine(plane.ToString());
            plane.Start();
            Console.WriteLine(plane.ToString());
            plane.IncreaseSpeed(750);
            Console.WriteLine(plane.ToString());
            plane.DecreaseSpeed(900);
            Console.WriteLine(plane.ToString());
            
            /* Land Vehicle
             var car = new LandVehicle(85, true, IVehicle.FuelType.Diesel, 4);
            Console.WriteLine(car.ToString());
            car.IncreaseSpeed(20);
            Console.WriteLine(car.ToString());
            car.IncreaseSpeed(400);
            Console.WriteLine(car.ToString());
            car.DecreaseSpeed(200);
            Console.WriteLine(car.ToString());
            car.Stop();
            Console.WriteLine(car.ToString());
            */
        }
    }
}