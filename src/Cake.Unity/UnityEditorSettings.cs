using Cake.Core;
using Cake.Core.Tooling;

namespace Cake.Unity
{
    internal class UnityEditorSettings : ToolSettings
    {
        public UnityEditorSettings(UnityEditorArguments arguments, ICakeEnvironment environment) =>
            ArgumentCustomization = builder => arguments.CustomizeCommandLineArguments(builder, environment);
    }
}
