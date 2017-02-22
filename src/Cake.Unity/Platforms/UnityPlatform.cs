using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Unity.Platforms
{
    public abstract class UnityPlatform : ToolSettings
    {
        public virtual void BuildArguments(ICakeContext context, ProcessArgumentBuilder builder)
        {
        }
    }
}