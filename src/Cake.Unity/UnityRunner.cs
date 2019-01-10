using Cake.Core.Utilities;
using System;
using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Core;
using Cake.Core.Tooling;
using Cake.Unity.Platforms;

namespace Cake.Unity
{
    /// <summary>
    /// Command-line arguments accepted by Unity Editor.
    /// </summary>
    public class UnityEditorArguments
    {
        /// <summary>
        /// <para>Run Unity in batch mode. You should always use this in conjunction with the other command line arguments, because it ensures no pop-up windows appear and eliminates the need for any human intervention. When an exception occurs during execution of the script code, the Asset server updates fail, or other operations fail, Unity immediately exits with return code 1.</para>
        /// <para>Note that in batch mode, Unity sends a minimal version of its log output to the console. However, the Log Files still contain the full log information. You cannot open a project in batch mode while the Editor has the same project open; only a single instance of Unity can run at a time.</para>
        /// <para>Tip: To check whether you are running the Editor or Standalone Player in batch mode, use the Application.isBatchMode operator.</para>
        /// </summary>
        public bool BatchMode { get; set; }

        /// <summary>
        /// Build a 32-bit standalone Windows player (for example, -buildWindowsPlayer path/to/your/build.exe).
        /// </summary>
        public FilePath BuildWindowsPlayer { get; set; }

        /// <summary>
        /// Specify where the Editor or Windows/Linux/OSX standalone log file are written. If the path is ommitted, OSX and Linux will write output to the console. Windows uses the path %LOCALAPPDATA%\Unity\Editor\Editor.log as a default.
        /// </summary>
        public FilePath LogFile { get; set; }

        /// <summary>
        /// Open the project at the given path.
        /// </summary>
        public DirectoryPath ProjectPath { get; set; }

        /// <summary>
        /// Quit the Unity Editor after other commands have finished executing. Note that this can cause error messages to be hidden (however, they still appear in the Editor.log file).
        /// </summary>
        public bool Quit { get; set; }

        internal ProcessArgumentBuilder CustomizeCommandLineArguments(ProcessArgumentBuilder builder, ICakeEnvironment environment)
        {
            if (BatchMode)
                builder
                    .Append("-batchmode");

            if (BuildWindowsPlayer != null)
                builder
                    .Append("-buildWindowsPlayer")
                    .AppendQuoted(BuildWindowsPlayer.MakeAbsolute(environment).FullPath);

            if (LogFile != null)
                builder
                    .Append("-logFile")
                    .AppendQuoted(LogFile.FullPath);

            if (ProjectPath != null)
                builder
                    .Append("-projectPath")
                    .AppendQuoted(ProjectPath.MakeAbsolute(environment).FullPath);

            if (Quit)
                builder
                    .Append("-quit");

            return builder;
        }
    }

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
