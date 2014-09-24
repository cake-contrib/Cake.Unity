using Cake.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.IO;
using Cake.Core;

namespace Cake.Unity
{
    public sealed class UnityRunner : Tool<IUnityPlatform>
    {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;

        public UnityRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner)
            : base(fileSystem, environment, processRunner)
        {
            _fileSystem = fileSystem;
            _environment = environment;
        }

        protected override string GetToolName()
        {
            return "Unity";
        }

        protected override FilePath GetDefaultToolPath(IUnityPlatform settings)
        {
            var programFilesPath = _environment.GetSpecialPath(SpecialPath.ProgramFilesX86);
            return programFilesPath.CombineWithFilePath("Unity/Editor/Unity.exe");
        }

        public void Run(ICakeContext context, DirectoryPath projectPath, IUnityPlatform platform)
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
