namespace Cake.Unity.Arguments
{
    public class GLESVersion
    {
        public enum Version
        {
            Auto,
            v30,
            v31,
            v32
        }

        private readonly Version _version;

        public GLESVersion(Version version) =>
            _version = version;

        public override string ToString()
        {
            switch (_version)
            {
                case Version.Auto: return "";
                case Version.v30: return "30";
                case Version.v31: return "31";
                case Version.v32: return "32";
            }
            return null;
        }
    }
}
