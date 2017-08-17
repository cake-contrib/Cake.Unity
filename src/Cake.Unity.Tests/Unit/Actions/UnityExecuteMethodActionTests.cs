using Cake.Core.IO;
using Cake.Unity.Actions;
using Cake.Unity.Tests.Fixtures;
using Xunit;

namespace Cake.Unity.Tests.Unit.Actions
{
    public sealed class UnityExecuteMethodActionTests
    {
        [Fact]
        public void Should_Pass_Method()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityExecuteMethodAction("ClassName.MethodName");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-executeMethod ClassName.MethodName", builder.Render());
        }
        [Fact]
        public void Should_Pass_Extra_Parameters()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityExecuteMethodAction("ClassName.MethodName", "-first", "-second");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-executeMethod ClassName.MethodName -first -second", builder.Render());
        }
    }
}
