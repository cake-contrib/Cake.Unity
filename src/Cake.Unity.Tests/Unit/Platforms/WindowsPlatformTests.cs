using Cake.Core.IO;
using Cake.Unity.Platforms;
using Cake.Unity.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cake.Unity.Tests.Unit.Platforms
{
    public sealed class WindowsPlatformTests
    {
        public sealed class TheBuildArgumentsMethod
        {
            [Fact]
            public void Should_Use_32_Bit_Platform_If_Nothing_Else_Specified()
            {
                // Given
                var context = UnityPlatformFixture.CreateContext();
                var builder = new ProcessArgumentBuilder();
                var platform = new WindowsPlatform("C:/Project/Game.exe");

                // When
                platform.BuildArguments(context, builder);

                // Then
                Assert.Equal("-buildWindowsPlayer \"C:/Project/Game.exe\"", builder.Render());
            }

            [Fact]
            public void Should_Make_Project_Path_Absolute()
            {
                // Given
                var context = UnityPlatformFixture.CreateContext();
                var builder = new ProcessArgumentBuilder();
                var platform = new WindowsPlatform("Project/Game.exe");

                // When
                platform.BuildArguments(context, builder);

                // Then
                Assert.Equal("-buildWindowsPlayer \"/Working/Project/Game.exe\"", builder.Render());
            }

            [Fact]
            public void Should_Add_NoGraphics_Switch_If_Specified()
            {
                // Given
                var context = UnityPlatformFixture.CreateContext();
                var builder = new ProcessArgumentBuilder();
                var platform = new WindowsPlatform("C:/Project/Game.exe");
                platform.NoGraphics = true;

                // When
                platform.BuildArguments(context, builder);

                // Then
                Assert.Equal("-nographics -buildWindowsPlayer \"C:/Project/Game.exe\"", builder.Render());
            }

            [Fact]
            public void Should_Use_64_Bit_Platform_If_Specified()
            {
                // Given
                var context = UnityPlatformFixture.CreateContext();
                var builder = new ProcessArgumentBuilder();
                var platform = new WindowsPlatform(true, "C:/Project/Game.exe");

                // When
                platform.BuildArguments(context, builder);

                // Then
                Assert.Equal("-buildWindows64Player \"C:/Project/Game.exe\"", builder.Render());
            }
        }
    }
}
