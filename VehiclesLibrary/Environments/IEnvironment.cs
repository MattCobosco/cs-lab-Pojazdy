namespace ClassLibrary.Environments
{
    public interface IEnvironment
    {
        public enum EnvironmentType
        {
            Air,
            Land,
            Water
        }

        public enum SpeedUnit
        {
            Kph,
            Knots,
            Mps
        }

        public EnvironmentType Type { get; }
        public SpeedUnit Unit { get; }
        public int MinSpeed { get; }
        public int MaxSpeed { get; }

        public static class Environments
        {
            private static readonly LandEnvironment _landEnvironment = new LandEnvironment();
            private static readonly WaterEnvironment _waterEnvironment = new WaterEnvironment();
            private static readonly AirEnvironment _airEnvironment = new AirEnvironment();
            public static readonly LandEnvironment LandEnvironment = _landEnvironment;
            public static readonly WaterEnvironment WaterEnvironment = _waterEnvironment;
            public static readonly AirEnvironment AirEnvironment = _airEnvironment;
        }
    }
}