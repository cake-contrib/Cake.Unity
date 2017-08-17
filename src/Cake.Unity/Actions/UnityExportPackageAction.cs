using Cake.Core;
using Cake.Core.IO;

namespace Cake.Unity.Actions
{
    /// <summary>
    /// Export a package, given a path (or set of given paths).
    /// In this example exportAssetPath is a folder (relative to to the Unity project root) to export from the Unity project,
    /// and exportFileName is the package name. Currently, this option only exports whole folders at a time.
    /// </summary>
    public class UnityExportPackageAction : UnityAction
    {
        private readonly FilePath _exportFileName;
        private readonly DirectoryPath[] _exportAssetPaths;

        public UnityExportPackageAction(FilePath exportFileName, params DirectoryPath[] exportAssetPaths)
        {
            _exportFileName = exportFileName;
            _exportAssetPaths = exportAssetPaths;
        }

        public override void BuildArguments(ICakeContext context, ProcessArgumentBuilder arguments)
        {
            base.BuildArguments(context, arguments);
            arguments.Append("-exportPackage");
            foreach (var exportAssetPath in _exportAssetPaths)
            {
                arguments.AppendQuoted(exportAssetPath.MakeAbsolute(context.Environment).FullPath);
            }
            arguments.AppendQuoted(_exportFileName.MakeAbsolute(context.Environment).FullPath);
        }
    }
}
