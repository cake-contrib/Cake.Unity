using Cake.Core;
using Cake.Core.IO;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Unity.Tests.Fixtures
{
    public sealed class UnityRunnerFixture
    {
        public IFileSystem FileSystem { get; set; }
        public IProcess Process { get; set; }
        public IProcessRunner ProcessRunner { get; set; }
        public ICakeEnvironment Environment { get; set; }

        public ICakeContext Context { get; set; }
        public DirectoryPath ProjectPath { get; set; }
        public IUnityPlatform Platform { get; set; }

        public bool ProjectPathExist { get; set; }

        public UnityRunnerFixture()
        {
            Context = Substitute.For<ICakeContext>();
            ProjectPath = new DirectoryPath("C:/Project");
            ProjectPathExist = true;
            Platform = Substitute.For<IUnityPlatform>();

            Process = Substitute.For<IProcess>();
            Process.GetExitCode().Returns(0);

            ProcessRunner = Substitute.For<IProcessRunner>();
            ProcessRunner.Start(Arg.Any<FilePath>(), Arg.Any<ProcessSettings>()).Returns(Process);

            Environment = Substitute.For<ICakeEnvironment>();
            Environment.WorkingDirectory = "/Working";
            Environment.GetSpecialPath(Arg.Is(SpecialPath.ProgramFilesX86)).Returns("C:/Program Files (x86)");

            FileSystem = Substitute.For<IFileSystem>();
            FileSystem.Exist(Arg.Is<FilePath>(a => a.FullPath == "C:/Program Files (x86)/Unity/Editor/Unity.exe")).Returns(true);
            FileSystem.Exist(Arg.Is<DirectoryPath>(a => ProjectPath != null && a.FullPath == ProjectPath.FullPath)).Returns((c) => ProjectPathExist);
        }

        public void ExecuteRunner()
        {
            var runner = new UnityRunner(FileSystem, Environment, ProcessRunner);
            runner.Run(Context, ProjectPath, Platform);
        }
    }
}
