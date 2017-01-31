using Cake.Core.Utilities;
using System;
using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Core;
using Cake.Core.Tooling;
using Cake.Unity.Platforms;

namespace Cake.Unity
{
    public sealed class UnityRunner : Core.Tooling.Tool<UnityPlatform>
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;

        public UnityRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            _fileSystem = fileSystem;
            _environment = environment;
        }

        protected override string GetToolName()
        {
            return "Unity";
        }

        protected override IEnumerable<FilePath> GetAlternativeToolPaths(UnityPlatform settings)
        {
            var programFilesPath = _environment.GetSpecialPath(SpecialPath.ProgramFilesX86);
            yield return programFilesPath.CombineWithFilePath("Unity/Editor/Unity.exe");
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "Unity.exe";
        }

        public void Run(ICakeContext context, DirectoryPath projectPath, UnityPlatform platform)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (projectPath == null)
            {
                throw new ArgumentNullException("projectPath");
            }
            if (platform == null)
            {
                throw new ArgumentNullException("platform");
            }

            // Make sure the project path exist.
            projectPath = projectPath.MakeAbsolute(_environment);
            if (!_fileSystem.Exist(projectPath))
            {
                const string format = "The Unity project {0} do not exist.";
                var message = string.Format(format, projectPath.FullPath);
                throw new CakeException(message);
            }

            // Build the arguments.
            var arguments = new ProcessArgumentBuilder();
            arguments.Append("-batchmode");
            arguments.Append("-quit");
            arguments.Append("-projectPath");
            arguments.AppendQuoted(projectPath.MakeAbsolute(_environment).FullPath);

            // Let the settings add it's own arguments.
            platform.BuildArguments(context, arguments);

            // Run the tool.
            Run(platform, arguments);
        }
    }
}
