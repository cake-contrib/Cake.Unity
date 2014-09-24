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
        private readonly bool _use64Bit;
        private readonly FilePath _outputPath;

        public bool NoGraphics { get; set; }

        public WindowsPlatform(FilePath outputPath)
            : this(false, outputPath)
        {
        }

        public WindowsPlatform(bool is64Bit, FilePath outputPath)
        {
            _use64Bit = is64Bit;
            _outputPath = outputPath;
        }

        public void BuildArguments(ICakeContext context, ProcessArgumentBuilder builder)
        {
            if (NoGraphics)
            {
                builder.Append("-nographics");
            }

            builder.Append(_use64Bit ? "-buildWindows64Player" : "-buildWindowsPlayer");
            builder.AppendQuoted(_outputPath.MakeAbsolute(context.Environment).FullPath);
        }
    }
}
