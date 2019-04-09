namespace Cake.Unity.Arguments
{
    public class GLCoreVersion
    {
        public enum Version
        {
            Auto,
            v32,
            v33,
            v40,
            v41,
            v42,
            v43,
            v44,
            v45
        }

        private readonly Version _version;

        public GLCoreVersion(Version version) =>
            _version = version;

        public override string ToString()
        {
            switch (_version)
            {
                case Version.Auto: return "";
                case Version.v32: return "32";
                case Version.v33: return "34";
                case Version.v40: return "40";
                case Version.v41: return "41";
                case Version.v42: return "42";
                case Version.v43: return "43";
                case Version.v44: return "44";
                case Version.v45: return "45";
            }
            return null;
        }
    }
}
