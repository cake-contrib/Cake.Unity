using Cake.Core;
using Cake.Core.Tooling;

namespace Cake.Unity
{
    internal class UnityEditorToolSettings : ToolSettings
    {
        public UnityEditorToolSettings(UnityEditorArguments arguments, ICakeEnvironment environment) =>
            ArgumentCustomization = builder => arguments.CustomizeCommandLineArguments(builder, environment);
    }
}
