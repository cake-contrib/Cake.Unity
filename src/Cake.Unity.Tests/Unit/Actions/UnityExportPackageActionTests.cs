using Cake.Core.IO;
using Cake.Unity.Actions;
using Cake.Unity.Tests.Fixtures;
using Xunit;

namespace Cake.Unity.Tests.Unit.Actions
{
    public sealed class UnityExportPackageActionTests
    {
        [Fact]
        public void Should_Pass_Export_Assets()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityExportPackageAction("C:/Result.unitypackage", "C:/Project/Assets/ToExport");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-exportPackage \"C:/Project/Assets/ToExport\" \"C:/Result.unitypackage\"", builder.Render());
        }

        [Fact]
        public void Should_Make_Paths_Absolute()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityExportPackageAction("Result.unitypackage", "Assets/ToExport");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-exportPackage \"/Working/Assets/ToExport\" \"/Working/Result.unitypackage\"", builder.Render());
        }
    }
}
