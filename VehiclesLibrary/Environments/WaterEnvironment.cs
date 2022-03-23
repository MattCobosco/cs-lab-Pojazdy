namespace ClassLibrary.Environments
{
    public class WaterEnvironment : IEnvironment
    {
        public IEnvironment.EnvironmentType Type => IEnvironment.EnvironmentType.Water;
        public IEnvironment.SpeedUnit Unit => IEnvironment.SpeedUnit.Knots;
        public int MinSpeed => 1;
        public int MaxSpeed => 40;
    }
}