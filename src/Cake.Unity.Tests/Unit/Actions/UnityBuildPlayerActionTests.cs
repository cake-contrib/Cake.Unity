using Cake.Core.IO;
using Cake.Unity.Actions;
using Cake.Unity.Tests.Fixtures;
using Xunit;

namespace Cake.Unity.Tests.Unit.Actions
{
    public sealed class UnityBuildPlayerActionTests
    {
        [Fact]
        public void Should_Use_Linux32Player_Player()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityBuildPlayerAction(UnityBuildPlayer.Linux32, "C:/Project/Game.exe");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-buildLinux32Player \"C:/Project/Game.exe\"", builder.Render());
        }

        [Fact]
        public void Should_Use_Linux64Player_Player()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityBuildPlayerAction(UnityBuildPlayer.Linux64, "C:/Project/Game.exe");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-buildLinux64Player \"C:/Project/Game.exe\"", builder.Render());
        }

        [Fact]
        public void Should_Use_LinuxUniversalPlayer_Player()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityBuildPlayerAction(UnityBuildPlayer.LinuxUniversal, "C:/Project/Game.exe");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-buildLinuxUniversalPlayer \"C:/Project/Game.exe\"", builder.Render());
        }

        [Fact]
        public void Should_Use_OSXPlayer_Player()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityBuildPlayerAction(UnityBuildPlayer.OSX, "C:/Project/Game.exe");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-buildOSXPlayer \"C:/Project/Game.exe\"", builder.Render());
        }

        [Fact]
        public void Should_Use_OSX64Player_Player()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityBuildPlayerAction(UnityBuildPlayer.OSX64, "C:/Project/Game.exe");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-buildOSX64Player \"C:/Project/Game.exe\"", builder.Render());
        }

        [Fact]
        public void Should_Use_OSXUniversa_Player()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityBuildPlayerAction(UnityBuildPlayer.OSXUniversal, "C:/Project/Game.exe");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-buildOSXUniversalPlayer \"C:/Project/Game.exe\"", builder.Render());
        }

        [Fact]
        public void Should_Use_Windows_Player()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityBuildPlayerAction(UnityBuildPlayer.Windows, "C:/Project/Game.exe");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-buildWindowsPlayer \"C:/Project/Game.exe\"", builder.Render());
        }
        [Fact]
        public void Should_Use_Windows64_Player()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityBuildPlayerAction(UnityBuildPlayer.Windows64, "C:/Project/Game.exe");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-buildWindows64Player \"C:/Project/Game.exe\"", builder.Render());
        }
            
        [Fact]
        public void Should_Make_Project_Path_Absolute()
        {
            // Given
            var context = UnityActionFixture.CreateContext();
            var builder = new ProcessArgumentBuilder();
            var platform = new UnityBuildPlayerAction(UnityBuildPlayer.Windows, "Project/Game.exe");

            // When
            platform.BuildArguments(context, builder);

            // Then
            Assert.Equal("-buildWindowsPlayer \"/Working/Project/Game.exe\"", builder.Render());
        }
    }
}
