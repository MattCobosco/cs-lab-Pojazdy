using System;
using System.Runtime.CompilerServices;
using ClassLibrary.Vehicles;

namespace cs_lab_Pojazdy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var plane = new AirVehicle(200, true, IVehicle.FuelType.Petrol);
            Console.WriteLine(plane.ToString());
            plane.IncreaseSpeed(1);
            Console.WriteLine(plane.ToString());
            plane.IncreaseSpeed(715);
            Console.WriteLine(plane.ToString());
            plane.DecreaseSpeed(700);
            Console.WriteLine(plane.ToString());
        }
    }
}