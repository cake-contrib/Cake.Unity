using Cake.Core.IO;
using Cake.Unity.Version;

namespace Cake.Unity
{
    public class UnityEditorDescriptor
    {
        public UnityEditorDescriptor(UnityVersion version, FilePath path)
        {
            Version = version;
            Path = path;
        }

        public UnityVersion Version { get; }
        public FilePath Path { get; }

        public override string ToString() => $"Unity {Version} @ {Path.FullPath}";
    }
}
