using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Unity.Actions
{
    public class UnityAction : ToolSettings
    {
        /// <summary>
        /// When running in batch mode, do not initialize the graphics device at all.
        /// </summary>
        public bool NoGraphics { get; set; }

        /// <summary>
        /// Allows the selection of an active build target before a project is loaded.
        /// </summary>
        public UnityBuildTarget BuildTarget { get; set; }

        public virtual void BuildArguments(ICakeContext context, ProcessArgumentBuilder arguments)
        {
            // When running in batch mode, do not initialize the graphics device at all.
            if (NoGraphics)
            {
                arguments.Append("-nographics");
            }

            // Allows the selection of an active build target before a project is loaded.
            if (BuildTarget != UnityBuildTarget.None)
            {
                arguments.Append("-buildTarget");
                arguments.Append(GetBuildTargetValue());
            }
        }

        private string GetBuildTargetValue()
        {
            switch (BuildTarget)
            {
            case UnityBuildTarget.StandaloneOSXUniversal: return "osx";
            case UnityBuildTarget.StandaloneWindows: return "win32";
            case UnityBuildTarget.iOS: return "ios";
            case UnityBuildTarget.Android: return "android";
            case UnityBuildTarget.StandaloneLinux: return "linux";
            case UnityBuildTarget.StandaloneWindows64: return "win64";
            case UnityBuildTarget.WebGL: return "webgl";
            case UnityBuildTarget.WSAPlayer: return "wsaplayer";
            case UnityBuildTarget.StandaloneLinux64: return "linux64";
            case UnityBuildTarget.Tizen: return "tizen";
            case UnityBuildTarget.PSP2: return "psp2";
            case UnityBuildTarget.PS4: return "ps4";
            case UnityBuildTarget.XboxOne: return "xboxone";
            case UnityBuildTarget.SamsungTV: return "samsungtv";
            default:
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}