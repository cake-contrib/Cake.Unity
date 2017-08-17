using Cake.Core.IO;
using Cake.Unity.Actions;
using Cake.Unity.Tests.Fixtures;
using Xunit;

namespace Cake.Unity.Tests.Unit.Actions
{
    public sealed class UnityImportPackageActionTests
    {
        [Fact]
        public void Should_Pass_Import_Package()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityImportPackageAction("C:/Import.unitypackage");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-importPackage \"C:/Import.unitypackage\"", builder.Render());
        }

        [Fact]
        public void Should_Make_Path_Absolute()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityImportPackageAction("Import.unitypackage");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-importPackage \"/Working/Import.unitypackage\"", builder.Render());
        }
    }
}
