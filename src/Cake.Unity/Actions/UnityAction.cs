using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Unity.Actions
{
    public abstract class UnityAction : ToolSettings
    {
        public abstract void BuildArguments(ICakeContext context, ProcessArgumentBuilder arguments);
    }
}