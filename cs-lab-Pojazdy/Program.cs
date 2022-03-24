using System;
using System.Runtime.CompilerServices;
using ClassLibrary.Vehicles;

namespace cs_lab_Pojazdy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /* AirVehicle
             var plane = new AirVehicle(200, true, IVehicle.FuelType.Petrol);
            Console.WriteLine(plane.ToString());
            plane.IncreaseSpeed(1);
            Console.WriteLine(plane.ToString());
            plane.IncreaseSpeed(715);
            Console.WriteLine(plane.ToString());
            plane.DecreaseSpeed(700);
            Console.WriteLine(plane.ToString());
            */

            
            var car = new LandVehicle(85, true, IVehicle.FuelType.Diesel, 4);
            Console.WriteLine(car.ToString());
            car.IncreaseSpeed(20);
            Console.WriteLine(car.ToString());
            car.IncreaseSpeed(400);
            Console.WriteLine(car.ToString());
            car.DecreaseSpeed(200);
            Console.WriteLine(car.ToString());
            car.DecreaseSpeed(300);
            Console.WriteLine(car.ToString());
        }
    }
}