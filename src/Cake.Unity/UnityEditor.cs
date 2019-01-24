using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Unity
{
    internal class UnityEditor : Tool<UnityEditorToolSettings>
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

        public void Run(UnityEditorDescriptor unityEditor, UnityEditorArguments arguments, UnityEditorSettings settings) =>
            Run(unityEditor.Path, arguments, settings);

        public void Run(FilePath unityEditorPath, UnityEditorArguments arguments, UnityEditorSettings settings)
        {
            ErrorIfRealTimeLogSetButLogFileNotSet(settings, arguments);
            WarnIfLogFileNotSet(arguments);

            if (settings.RealTimeLog && arguments.LogFile != null)
                RunWithRealTimeLog(unityEditorPath, arguments);
            else
                RunWithLogForwardOnError(unityEditorPath, arguments);
        }

        private void RunWithRealTimeLog(FilePath unityEditorPath, UnityEditorArguments arguments)
        {
            var logForwardCancellation = new CancellationTokenSource();

            var process = RunProcess(ToolSettings(unityEditorPath, arguments));

            Task.Run(() =>
            {
                process.WaitForExit();
                logForwardCancellation.Cancel();
            });

            ForwardLogFileToOutputUntilCancel(arguments.LogFile, logForwardCancellation.Token);

            ProcessExitCode(process.GetExitCode());
        }

        private void RunWithLogForwardOnError(FilePath unityEditorPath, UnityEditorArguments arguments)
        {
            try
            {
                Run(ToolSettings(unityEditorPath, arguments));
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
                    ForwardLogFileToOutputInOnePass(arguments.LogFile);
                }

                throw;
            }
        }

        private UnityEditorToolSettings ToolSettings(FilePath unityEditorPath, UnityEditorArguments arguments) =>
            new UnityEditorToolSettings(arguments, environment) { ToolPath = unityEditorPath };

        private void Run(UnityEditorToolSettings settings) => Run(settings, new ProcessArgumentBuilder());
        private IProcess RunProcess(UnityEditorToolSettings settings) => RunProcess(settings, new ProcessArgumentBuilder());

        private void ForwardLogFileToOutputUntilCancel(FilePath logPath, CancellationToken cancellationToken)
        {
            foreach (var line in ReadLogUntilCancel(logPath, cancellationToken))
                ForwardLogLineToOutput(line);
        }

        private void ForwardLogFileToOutputInOnePass(FilePath logPath)
        {
            var logFile = fileSystem.GetFile(logPath);

            if (!logFile.Exists)
            {
                log.Warning("Unity Editor log file not found: {0}", logPath);
                return;
            }

            foreach (var line in ReadLogSafeInOnePass(logFile))
                ForwardLogLineToOutput(line);
        }

        private void ForwardLogLineToOutput(string line)
        {
            if (IsError(line))
                log.Error(line);
            else if (IsWarning(line))
                log.Warning(line);
            else
                log.Information(line);
        }

        private static bool IsError(string line) => IsCSharpCompilerError(line);
        private static bool IsWarning(string line) => IsCSharpCompilerWarning(line);

        private static bool IsCSharpCompilerError(string line) => line.Contains(": error CS");
        private static bool IsCSharpCompilerWarning(string line) => line.Contains(": warning CS");

        private IEnumerable<string> ReadLogUntilCancel(FilePath logPath, CancellationToken cancellationToken)
        {
            bool LogExists() => fileSystem.Exist(logPath);
            bool ShouldWork() => !cancellationToken.IsCancellationRequested;
            void Sleep() => Thread.Sleep(TimeSpan.FromSeconds(1));

            while (!LogExists() && ShouldWork())
                Sleep();

            if (!LogExists())
            {
                log.Warning("Unity Editor log file not found: {0}", logPath);
                yield break;
            }

            using (var stream = fileSystem.GetFile(logPath).Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                bool HasLinesToRead() => !reader.EndOfStream;

                while (HasLinesToRead() || ShouldWork())
                {
                    if (HasLinesToRead())
                        yield return reader.ReadLine();
                    else
                        Sleep();
                }
            }
        }

        private static IEnumerable<string> ReadLogSafeInOnePass(IFile file)
        {
            using (var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
                while (!reader.EndOfStream)
                    yield return reader.ReadLine();
        }

        private void ErrorIfRealTimeLogSetButLogFileNotSet(UnityEditorSettings settings, UnityEditorArguments arguments)
        {
            if (settings.RealTimeLog && arguments.LogFile == null)
            {
                log.Error("Cannot forward log in real time because LogFile is not specified.");
            }
        }

        private void WarnIfLogFileNotSet(UnityEditorArguments arguments)
        {
            if (arguments.LogFile == null)
            {
                log.Warning("LogFile is not specified by Unity Editor arguments.");
                log.Warning("Please specify it for ability to forward Unity log to console.");
            }
        }
    }
}
