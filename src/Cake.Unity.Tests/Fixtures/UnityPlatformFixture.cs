using Cake.Core;
using Cake.Core.IO;
using Cake.Unity.Platforms;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Unity.Tests.Fixtures
{
    public sealed class UnityPlatformFixture
    {
        public static ICakeContext CreateContext()
        {
            var environment = Substitute.For<ICakeEnvironment>();
            environment.WorkingDirectory = "/Working";

            var context = Substitute.For<ICakeContext>();
            context.Environment.Returns((c) => environment);
            return context;
        }
    }
}
