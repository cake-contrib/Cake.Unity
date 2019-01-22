using System.Collections.Generic;
using System.IO;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Unity
{
    internal class UnityEditor : Tool<UnityEditorSettings>
    {
        private readonly IFileSystem fileSystem;
        private readonly ICakeEnvironment environment;
        private readonly ICakeLog log;

        public UnityEditor(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools, ICakeLog log)
            : base(fileSystem, environment, processRunner, tools)
        {
            this.fileSystem = fileSystem;
            this.environment = environment;
            this.log = log;
        }

        protected override string GetToolName() => "Unity Editor";

        protected override IEnumerable<string> GetToolExecutableNames() => new[] { "Unity.exe" };

        public void Run(FilePath unityEditorPath, UnityEditorArguments arguments)
        {
            WarnIfLogLocationNotSet(arguments);

            try
            {
                Run(new UnityEditorSettings(arguments, environment) { ToolPath = unityEditorPath });
            }
            catch
            {
                if (arguments.LogFile == null)
                {
                    log.Error("Execution of Unity Editor failed.");
                    log.Warning("Cannot forward log file to output because LogFile argument is missing.");
                }
                else
                {
                    log.Error("Execution of Unity Editor failed.");
                    log.Error("Please analyze log below for the reasons of failure.");
                    ForwardLogFileToOutput(arguments.LogFile);
                }

                throw;
            }
        }

        private void Run(UnityEditorSettings settings) => Run(settings, new ProcessArgumentBuilder());

        private void ForwardLogFileToOutput(FilePath logPath)
        {
            var logFile = fileSystem.GetFile(logPath);

            if (!logFile.Exists)
            {
                log.Warning("Unity Editor log file not found: {0}", logPath);
                return;
            }

            foreach (var line in ReadLogSafe(logFile))
            {
                if (IsError(line))
                    log.Error(line);
                else if (IsWarning(line))
                    log.Warning(line);
                else
                    log.Information(line);
            }
        }

        private static bool IsError(string line) => IsCSharpCompilerError(line);
        private static bool IsWarning(string line) => IsCSharpCompilerWarning(line);

        private static bool IsCSharpCompilerError(string line) => line.Contains(": error CS");
        private static bool IsCSharpCompilerWarning(string line) => line.Contains(": warning CS");

        private static IEnumerable<string> ReadLogSafe(IFile file)
        {
            using (var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
                while (!reader.EndOfStream)
                    yield return reader.ReadLine();
        }

        private void WarnIfLogLocationNotSet(UnityEditorArguments arguments)
        {
            if (arguments.LogFile == null)
            {
                log.Warning("LogFile is not specified by Unity Editor arguments.");
                log.Warning("Please specify it for ability to forward Unity log to console.");
            }
        }
    }
}
