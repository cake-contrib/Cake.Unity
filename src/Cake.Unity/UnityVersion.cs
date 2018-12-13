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

        public UnityVersion(int year, int stream, int update, char suffixCharacter, int suffixNumber)
            : this(year, stream, update)
        {
            this.SuffixCharacter = suffixCharacter;
            this.SuffixNumber = suffixNumber;
        }

        public int Year { get; }
        public int Stream { get; }
        public int Update { get; }
        public char? SuffixCharacter { get; }
        public int? SuffixNumber { get; }

        public override string ToString() =>
            SuffixCharacter.HasValue && SuffixNumber.HasValue
                ? $"{Year}.{Stream}.{Update}{SuffixCharacter}{SuffixNumber}"
                : $"{Year}.{Stream}.{Update}";
    }
}
