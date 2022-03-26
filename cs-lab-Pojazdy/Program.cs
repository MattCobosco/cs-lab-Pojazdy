
using System;
using ClassLibrary.Vehicles;

namespace cs_lab_Pojazdy
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IVehicle airplane = new AirVehicle(true, 500, IVehicle.FuelType.Avgas);
            airplane.Start();
            Console.WriteLine(airplane.ToString());
            airplane.Accelerate(15);
            Console.WriteLine(airplane.ToString());
            airplane.Decelerate(10);
            Console.WriteLine(airplane.ToString());
            airplane.Stop();
            Console.WriteLine(airplane.ToString());
        }
    }
}