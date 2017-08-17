using System;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Unity.Actions
{
    public class UnityBuildPlayerAction : UnityAction
    {
        private readonly UnityBuildPlayer _player;
        private readonly FilePath _outputPath;

        public UnityBuildPlayerAction(UnityBuildPlayer player, FilePath outputPath)
        {
            _player = player;
            _outputPath = outputPath;
        }

        public override void BuildArguments(ICakeContext context, ProcessArgumentBuilder arguments)
        {
            base.BuildArguments(context, arguments);
            arguments.Append(GetBuildPlayerCommend());
            arguments.AppendQuoted(_outputPath.MakeAbsolute(context.Environment).FullPath);
        }

        private string GetBuildPlayerCommend()
        {
            switch (_player)
            {
            case UnityBuildPlayer.Linux32Player: return "-buildLinux32Player";
            case UnityBuildPlayer.Linux64Player: return "-buildLinux64Player";
            case UnityBuildPlayer.LinuxUniversalPlayer: return "-buildLinuxUniversalPlayer";
            case UnityBuildPlayer.OSXPlayer: return "-buildOSXPlayer";
            case UnityBuildPlayer.OSX64Player: return "-buildOSX64Player";
            case UnityBuildPlayer.OSXUniversalPlayer: return "-buildOSXUniversalPlayer";
            case UnityBuildPlayer.WindowsPlayer: return "-buildWindowsPlayer";
            case UnityBuildPlayer.Windows64Player: return "-buildWindows64Player";
            default:
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
