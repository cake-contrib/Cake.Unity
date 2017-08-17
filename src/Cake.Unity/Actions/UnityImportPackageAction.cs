using Cake.Core;
using Cake.Core.IO;

namespace Cake.Unity.Actions
{
    /// <summary>
    /// Import the given package. No import dialog is shown.
    /// </summary>
    public class UnityImportPackageAction : UnityAction
    {
        private readonly FilePath _packagePath;

        public UnityImportPackageAction(FilePath packagePath)
        {
            _packagePath = packagePath;
        }

        public override void BuildArguments(ICakeContext context, ProcessArgumentBuilder arguments)
        {
            base.BuildArguments(context, arguments);
            arguments.Append("-importPackage");
            arguments.AppendQuoted(_packagePath.MakeAbsolute(context.Environment).FullPath);
        }
    }
}
