using Cake.Core.IO;

namespace Cake.Unity
{
    public class UnityEditorDescriptor
    {
        public UnityEditorDescriptor(string version, FilePath path)
        {
            Version = version;
            Path = path;
        }

        public string Version { get; }
        public FilePath Path { get; }

        public override string ToString() => $"Unity {Version} @ {Path.FullPath}";
    }
}
