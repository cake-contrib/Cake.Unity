using Cake.Core;
using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Unity.Platforms
{
    public sealed class WindowsPlatform : IUnityPlatform
    {
        private readonly FilePath _outputPath;

        public bool NoGraphics { get; set; }
        public UnityPlatformTarget PlatformTarget { get; set; }

        public WindowsPlatform(FilePath outputPath)
        {
            _outputPath = outputPath;
            PlatformTarget = UnityPlatformTarget.x86;
        }

        public void BuildArguments(ICakeContext context, ProcessArgumentBuilder builder)
        {
            if (NoGraphics)
            {
                builder.Append("-nographics");
            }

            builder.Append(PlatformTarget == UnityPlatformTarget.x64 ? "-buildWindows64Player" : "-buildWindowsPlayer");
            builder.AppendQuoted(_outputPath.MakeAbsolute(context.Environment).FullPath);
        }
    }
}
