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

        public static class Environments
        {
            private static LandEnvironment _landEnvironment = new LandEnvironment();
            private static WaterEnvironment _waterEnvironment = new WaterEnvironment();
            private static AirEnvironment _airEnvironment = new AirEnvironment();
            public static LandEnvironment LandEnvironment = _landEnvironment;
            public static WaterEnvironment WaterEnvironment = _waterEnvironment;
            public static AirEnvironment AirEnvironment = _airEnvironment;
        }

        public EnvironmentType Type { get; }
        public SpeedUnit Unit { get; }
        public int MinSpeed { get; }
        public int MaxSpeed { get; }
    }
}