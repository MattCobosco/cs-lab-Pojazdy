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
    }
}