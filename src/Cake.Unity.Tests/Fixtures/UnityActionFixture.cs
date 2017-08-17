using Cake.Core;
using NSubstitute;

namespace Cake.Unity.Tests.Fixtures
{
    public sealed class UnityActionFixture
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
