namespace Cake.Unity
{
    public class UnityVersion
    {
        public UnityVersion(int year, int stream, int update)
        {
            this.Year = year;
            this.Stream = stream;
            this.Update = update;
        }

        public int Year { get; }
        public int Stream { get; }
        public int Update { get; }

        public override string ToString() => $"{Year}.{Stream}.{Update}";
    }
}
