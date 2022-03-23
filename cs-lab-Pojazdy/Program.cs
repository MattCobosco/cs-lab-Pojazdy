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
            plane.IncreaseSpeed(50);
            Console.WriteLine(plane.ToString());
        }
    }
}