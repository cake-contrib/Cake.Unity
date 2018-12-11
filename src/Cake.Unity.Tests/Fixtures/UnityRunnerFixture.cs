using Cake.Core;
using Cake.Core.IO;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Tooling;
using Cake.Testing;
using Cake.Unity.Platforms;

namespace Cake.Unity.Tests.Fixtures
{
    public sealed class UnityRunnerFixture
    {
        public FakeFileSystem FileSystem { get; set; }
        public FakeProcess Process { get; set; }
        public IProcessRunner ProcessRunner { get; set; }
        public FakeEnvironment Environment { get; set; }
        public IToolLocator Tools { get; set; }

        public ICakeContext Context { get; set; }
        public DirectoryPath ProjectPath { get; set; }
        public UnityPlatform Platform { get; set; }

        public bool DefaultToolPathExist { get; set; }
        public bool ProjectPathExist { get; set; }

        public UnityRunnerFixture()
        {
            ProjectPath = new DirectoryPath("C:/Project");
            Platform = Substitute.For<UnityPlatform>();

            DefaultToolPathExist = true;
            ProjectPathExist = true;

            Process = new FakeProcess();

            ProcessRunner = Substitute.For<IProcessRunner>();
            ProcessRunner.Start(Arg.Any<FilePath>(), Arg.Any<ProcessSettings>()).Returns(Process);
             
            Environment = new FakeEnvironment(PlatformFamily.Windows)
            {
                WorkingDirectory = "/Working"
            };
            Environment.SetSpecialPath(SpecialPath.ProgramFilesX86,"C:/Program Files (x86)");

            FileSystem = new FakeFileSystem(Environment);
            var globber = new Globber(FileSystem, Environment);
            Tools = new ToolLocator(Environment, new ToolRepository(Environment), new ToolResolutionStrategy(FileSystem, Environment, globber, new FakeConfiguration()));
            Context = new CakeContext(FileSystem, Environment, globber, new FakeLog(), Substitute.For<ICakeArguments>(), ProcessRunner, Substitute.For<IRegistry>(), Tools, Substitute.For<ICakeDataService>());
        }

        public void ExecuteRunner()
        {
            if (DefaultToolPathExist)
            {
                FileSystem.CreateFile("C:/Program Files (x86)/Unity/Editor/Unity.exe");
            }
            if (ProjectPathExist && ProjectPath!=null)
            {
                FileSystem.CreateDirectory(ProjectPath);
            }
            var runner = new UnityRunner(FileSystem, Environment, ProcessRunner, Tools);
            runner.Run(Context, ProjectPath, Platform);
        }
    }
}
