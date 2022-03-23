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

        public static int Speed { get; set; } // in kph regardless of environment
        public VehicleState State => Speed > 0 ? VehicleState.Moving : VehicleState.Stationary;
        public bool HasEngine { get; }
        public int Horsepower { get; }

        public static IEnvironment CurrentEnvironment { get; }

        public void IncreaseSpeed(int change)
        {
            if (Speed + change > CurrentEnvironment.MaxSpeed)
                Speed = CurrentEnvironment.MaxSpeed;
            else
                Speed += change;
        }

        public void DecreaseSpeed(int change)
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
            if (CurrentEnvironment.Type == IEnvironment.EnvironmentType.Water) DecreaseSpeed(Speed);
        }
        
        public int GetConvertedSpeed()
        {
            return CurrentEnvironment.Type switch
            {
                IEnvironment.EnvironmentType.Air => (int)(Speed * 3.6),
                IEnvironment.EnvironmentType.Water => (int)(Speed * 1.852),
                IEnvironment.EnvironmentType.Land => Speed,
                _ => throw new Exception()
            };
        }
    }
}