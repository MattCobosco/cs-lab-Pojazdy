using System;
using System.Xml.Schema;
using ClassLibrary.Environments;

namespace ClassLibrary.Vehicles
{
    public interface IVehicle
    {
        public enum FuelType
        {
            Petrol,
            Lpg,
            Diesel,
            Electric,
            Other,
            None
        }

        public enum VehicleState
        {
            Moving,
            Stationary
        }

        public static double Speed { get; set; }
        public VehicleState State { get; }
        public bool HasEngine { get; }
        public int Horsepower { get; }

        public static IEnvironment CurrentEnvironment { get; }

        public void IncreaseSpeed(double change)
        {
            if (Speed + change > CurrentEnvironment.MaxSpeed)
                Speed = CurrentEnvironment.MaxSpeed;
            else
                Speed += change;
        }

        public void DecreaseSpeed(double change)
        {
            if (Speed - change < CurrentEnvironment.MinSpeed)
                Speed = 0;
            else
                Speed -= change;
        }

        public void Start()
        {
            IncreaseSpeed(CurrentEnvironment.MinSpeed);
        }

        public void Stop()
        {
            if (CurrentEnvironment.Type == IEnvironment.EnvironmentType.Water) 
                DecreaseSpeed(Speed);
        }
        
        public double GetConvertedSpeed(IEnvironment.SpeedUnit unit, double speed)
        {
            return unit switch
            {
                IEnvironment.SpeedUnit.Knots => speed / 1.852,
                IEnvironment.SpeedUnit.Mps => speed / 3.6,
                _ => speed
            };
        }
    }
}