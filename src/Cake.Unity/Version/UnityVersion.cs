using System.Collections.Generic;
using static Cake.Unity.Version.UnityReleaseStage;

namespace Cake.Unity.Version
{
    public class UnityVersion
    {
        private static readonly Dictionary<char, UnityReleaseStage> SuffixToStage = new Dictionary<char, UnityReleaseStage>
        {
            { 'a', Alpha },
            { 'b', Beta },
            { 'p', Patch },
            { 'f', Final },
        };

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

        public UnityReleaseStage Stage =>
            SuffixCharacter.HasValue && SuffixToStage.ContainsKey(SuffixCharacter.Value)
                ? SuffixToStage[SuffixCharacter.Value]
                : Unknown;

        public override string ToString() =>
            SuffixCharacter.HasValue && SuffixNumber.HasValue
                ? $"{Year}.{Stream}.{Update}{SuffixCharacter}{SuffixNumber}"
                : $"{Year}.{Stream}.{Update}";

        public static UnityVersion Parse(string s)
        {
            var version = s.Split('.');
            int charPos = (int)FirstNotDigit(version[2]);

            return new UnityVersion(
                year: int.Parse(version[0]),
                stream: int.Parse(version[1]),
                update: int.Parse(version[2].Substring(0, charPos)),
                suffixCharacter: version[2][charPos],
                suffixNumber: int.Parse(version[2].Substring(charPos + 1)));
        }

        private static int? FirstNotDigit(string str)
        {
            for (int i = 0; i < str.Length; ++i)
                if (!char.IsDigit(str[i]))
                    return i;
            return null;
        }

        public bool Equals(UnityVersion other) =>
            Year == other.Year &&
            Stream == other.Stream &&
            Update == other.Update &&
            SuffixCharacter == other.SuffixCharacter &&
            SuffixNumber == other.SuffixNumber;
    }
}
