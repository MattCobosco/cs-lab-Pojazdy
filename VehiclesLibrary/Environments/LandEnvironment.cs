namespace ClassLibrary.Environments
{
    public class LandEnvironment : IEnvironment
    {
        public IEnvironment.EnvironmentType Type => IEnvironment.EnvironmentType.Land;
        public IEnvironment.SpeedUnit Unit => IEnvironment.SpeedUnit.Kph;
        public int MinSpeed => 1;
        public int MaxSpeed => 350;
    }
}