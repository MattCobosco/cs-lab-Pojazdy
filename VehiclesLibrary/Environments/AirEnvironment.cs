namespace ClassLibrary.Environments
{
    public class AirEnvironment : IEnvironment
    {
        public IEnvironment.EnvironmentType Type => IEnvironment.EnvironmentType.Air;
        public IEnvironment.SpeedUnit Unit => IEnvironment.SpeedUnit.Mps;
        public int MinSpeed => 20;
        public int MaxSpeed => 200;
    }
}