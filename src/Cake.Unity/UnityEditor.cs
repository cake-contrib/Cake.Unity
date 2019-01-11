using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Unity
{
    internal class UnityEditor : Tool<UnityEditorSettings>
    {
        private readonly ICakeEnvironment environment;

        public UnityEditor(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools) =>
            this.environment = environment;

        protected override string GetToolName() => "Unity Editor";

        protected override IEnumerable<string> GetToolExecutableNames() => new[] { "Unity.exe" };

        public void Run(FilePath unityEditorPath, UnityEditorArguments arguments) =>
            Run(
                new UnityEditorSettings(arguments, environment)
                {
                    ToolPath = unityEditorPath,
                },
                new ProcessArgumentBuilder());
    }
}
