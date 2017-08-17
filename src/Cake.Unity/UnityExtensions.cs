using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.Unity.Actions;

namespace Cake.Unity
{
    [CakeAliasCategory("Unity")]
    public static class UnityExtensions
    {
        [CakeMethodAlias]
        public static void UnityBuild(this ICakeContext context, DirectoryPath projectPath, UnityBuildPlayer player, FilePath outputPath)
        {
            var tool = new UnityRunner(context);
            var action = new UnityBuildPlayerAction(player, outputPath);
            tool.Run(context, projectPath, action);
        }

        [CakeMethodAlias]
        public static void UnityExecuteMethod(this ICakeContext context, DirectoryPath projectPath, string method, params string[] extraParameters)
        {
            var tool = new UnityRunner(context);
            var action = new UnityExecuteMethodAction(method, extraParameters);
            tool.Run(context, projectPath, action);
        }

        [CakeMethodAlias]
        public static void UnityExportPackage(this ICakeContext context, DirectoryPath projectPath, FilePath exportFileName, params DirectoryPath[] exportAssetPaths)
        {
            var tool = new UnityRunner(context);
            var action = new UnityExportPackageAction(exportFileName, exportAssetPaths);
            tool.Run(context, projectPath, action);
        }

        [CakeMethodAlias]
        public static void UnityImportPackage(this ICakeContext context, DirectoryPath projectPath, FilePath packagePath)
        {
            var tool = new UnityRunner(context);
            var action = new UnityImportPackageAction(packagePath);
            tool.Run(context, projectPath, action);
        }
    }
}
