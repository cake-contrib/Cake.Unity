using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Unity.Actions;

namespace Cake.Unity
{
    public sealed class UnityRunner : Tool<UnityAction>
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;

        public UnityRunner(ICakeContext context)
            : this(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools)
        {
        }

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

        protected override IEnumerable<FilePath> GetAlternativeToolPaths(UnityAction settings)
        {
            switch (_environment.Platform.Family)
            {
            case PlatformFamily.Windows:
                yield return _environment.GetSpecialPath(SpecialPath.ProgramFiles)
                    .CombineWithFilePath("Unity/Editor/Unity.exe");
                yield return _environment.GetSpecialPath(SpecialPath.ProgramFilesX86)
                    .CombineWithFilePath("Unity/Editor/Unity.exe");
                break;
            case PlatformFamily.OSX:
                yield return "/Applications/Unity/Unity.app/Contents/MacOS/Unity";
                break;
            }
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "Unity.exe";
        }

        public void Run(ICakeContext context, DirectoryPath projectPath, UnityAction platform)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (projectPath == null)
            {
                throw new ArgumentNullException(nameof(projectPath));
            }
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
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
            // https://docs.unity3d.com/Manual/CommandLineArguments.html
            var arguments = new ProcessArgumentBuilder();

            // Run Unity in batch mode.
            arguments.Append("-batchmode");

            // Quit the Unity Editor after other commands have finished executing.
            arguments.Append("-quit");

            // Open the project at the given path.
            arguments.Append("-projectPath");
            arguments.AppendQuoted(projectPath.MakeAbsolute(_environment).FullPath);

            // Let the action add it's own arguments.
            platform.BuildArguments(context, arguments);

            // Run the tool.
            Run(platform, arguments);
        }
    }
}
