using Cake.Core.IO;
using Cake.Unity.Version;

namespace Cake.Unity
{
    /// <summary>
    /// Information about Unity Editor.
    /// </summary>
    public class UnityEditorDescriptor
    {
        internal UnityEditorDescriptor(UnityVersion version, FilePath path)
        {
            Version = version;
            Path = path;
        }

        /// <summary>Unity version.</summary>
        public UnityVersion Version { get; }

        /// <summary>Path to executable.</summary>
        public FilePath Path { get; }

        public override string ToString() => $"Unity {Version} @ {Path.FullPath}";
    }
}
