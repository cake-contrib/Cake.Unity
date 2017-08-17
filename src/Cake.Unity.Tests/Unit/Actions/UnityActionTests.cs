using Cake.Core.IO;
using Cake.Unity.Actions;
using Cake.Unity.Tests.Fixtures;
using Xunit;

namespace Cake.Unity.Tests.Unit.Actions
{
    public sealed class UnityActionTests
    {
        [Fact]
        public void Should_Add_NoGraphics_Switch_If_Specified()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityAction();
            platform.NoGraphics = true;

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-nographics", builder.Render());
        }
    }
}
