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
            arguments.Append(GetBuildPlayerCommend());
            arguments.AppendQuoted(_outputPath.MakeAbsolute(context.Environment).FullPath);
        }

        private string GetBuildPlayerCommend()
        {
            switch (_player)
            {
            case UnityBuildPlayer.Linux32Player: return "-buildLinux32Player";
            case UnityBuildPlayer.Linux64Player: return "-buildLinux32Player";
            case UnityBuildPlayer.LinuxUniversalPlayer: return "-buildLinux32Player";
            case UnityBuildPlayer.OSXPlayer: return "-buildLinux32Player";
            case UnityBuildPlayer.OSX64Player: return "-buildLinux32Player";
            case UnityBuildPlayer.OSXUniversalPlayer: return "-buildLinux32Player";
            case UnityBuildPlayer.WindowsPlayer: return "-buildLinux32Player";
            case UnityBuildPlayer.Windows64Player: return "-buildLinux32Player";
            default:
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
