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
        public static void UnityBuildPlayer(this ICakeContext context, DirectoryPath projectPath, UnityBuildPlayer player, FilePath outputPath)
        {
            var tool = new UnityRunner(context);
            var action = new UnityBuildPlayerAction(player, outputPath);
            tool.Run(context, projectPath, action);
        }
    }
}
