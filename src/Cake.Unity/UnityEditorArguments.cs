using System;
using System.Dynamic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Unity.Arguments;

namespace Cake.Unity
{
    /// <summary>
    /// Command-line arguments accepted by Unity Editor.
    /// </summary>
    public class UnityEditorArguments
    {
        private readonly ExpandoObject customArguments = new ExpandoObject();

        /// <summary>
        /// Force an update of the project in the Asset Server given by IP:port. The port is optional, and if not given it is assumed to be the standard one (10733). It is advisable to use this command in conjunction with the -projectPath argument to ensure you are working with the correct project. If you don’t give a project name, then the command line uses the last project opened by Unity. If no project exists at the path -projectPath gives, then the command line creates one automatically.
        /// </summary>
        public AssetServerUpdate AssetServerUpdate { get; set; }

        /// <summary>
        /// <para>Run Unity in batch mode. You should always use this in conjunction with the other command line arguments, because it ensures no pop-up windows appear and eliminates the need for any human intervention. When an exception occurs during execution of the script code, the Asset server updates fail, or other operations fail, Unity immediately exits with return code 1. </para>
        /// <para>Note that in batch mode, Unity sends a minimal version of its log output to the console. However, the Log Files still contain the full log information. You cannot open a project in batch mode while the Editor has the same project open; only a single instance of Unity can run at a time. </para>
        /// <para>Tip: To check whether you are running the Editor or Standalone Player in batch mode, use the Application.isBatchMode operator. </para>
        /// </summary>
        public bool BatchMode { get; set; } = true;

        /// <summary>
        /// Build a 32-bit standalone Linux player (for example, -buildLinux32Player path/to/your/build).
        /// </summary>
        public FilePath BuildLinux32Player { get; set; }

        /// <summary>
        /// Build a 64-bit standalone Linux player (for example, -buildLinux64Player path/to/your/build)
        /// </summary>
        public FilePath BuildLinux64Player { get; set; }

        /// <summary>
        /// Build a combined 32-bit and 64-bit standalone Linux player (for example, -buildLinuxUniversalPlayer path/to/your/build)
        /// </summary>
        public FilePath BuildLinuxUniversalPlayer { get; set; }

        /// <summary>
        /// Build a 32-bit standalone Mac OSX player (for example, -buildOSXPlayer path/to/your/build.app)
        /// </summary>
        public FilePath BuildOSXPlayer { get; set; }

        /// <summary>
        /// Build a 64-bit standalone Mac OSX player (for example, -buildOSX64Player path/to/your/build.app)
        /// </summary>
        public FilePath BuildOSX64Player { get; set; }

        /// <summary>
        /// Build a combined 32-bit and 64-bit standalone Mac OSX player (for example, -buildOSXUniversalPlayer path/to/your/build.app)
        /// </summary>
        public FilePath BuildOSXUniversalPlayer { get; set; }

        /// <summary>
        /// Allows the selection of an active build target before loading a project.
        /// </summary>
        public BuildTarget? BuildTarget { get; set; }

        /// <summary>
        /// Allows the selection of an active build target before loading a project (with string).
        /// </summary>
        public string CustomBuildTarget { get; set; }

        /// <summary>
        /// Build a 32-bit standalone Windows player (for example, -buildWindowsPlayer path/to/your/build.exe).
        /// </summary>
        public FilePath BuildWindowsPlayer { get; set; }

        /// <summary>
        /// Build a 64-bit standalone Windows player (for example, -buildWindows64Player path/to/your/build.exe)
        /// </summary>
        public FilePath BuildWindows64Player { get; set; }

        /// <summary>
        /// Detailed debugging feature. StackTraceLogging allows you to allow detailed logging. (for example, -stackTraceLogType Full)
        /// </summary>
        public StackTraceLogType? StackTraceLogType { get; set; }

        /// <summary>
        /// Connect to the specified Cache Server on startup, overriding any configuration stored in the Editor Preferences.
        /// Set string as host:port
        /// </summary>
        public string CacheServerIPAddress { get; set; }

        /// <summary>
        /// Create an empty project at the given path.
        /// </summary>
        public FilePath CreateProject { get; set; }

        /// <summary>
        /// Filter editor tests by categories. Separate test categories with a comma.
        /// </summary>
        public string EditorTestsCategories { get; set; }

        /// <summary>
        /// Filter editor tests by names. Separate test names with a comma.
        /// </summary>
        public string EditorTestsFilter { get; set; }

        /// <summary>
        /// Path location to place the result file. If the path is a folder, the command line uses a default file name. If not specified, it places the results in the project’s root folder.
        /// </summary>
        public FilePath TestResults { get; set; }

        /// <summary>
        /// Execute the static method as soon as Unity opens the project, and after the optional Asset server update is complete. You can use this to do tasks such as continous integration, performing Unit Tests, making builds or preparing data. To return an error from the command line process, either throw an exception which causes Unity to exit with return code 1, or call EditorApplication.Exit with a non-zero return code. To pass parameters, add them to the command line and retrieve them inside the function using System.Environment.GetCommandLineArgs. To use -executeMethod, you need to place the enclosing script in an Editor folder. The method you execute must be defined as static.
        /// </summary>
        public string ExecuteMethod { get; set; }

        /// <summary>
        /// Export a package, given a path (or set of given paths). Asset paths are set relative to the Unity project root.
        /// Currently, this option only exports whole folders at a time. You normally need to use this command with the -projectPath argument.
        /// </summary>
        public ExportPackage ExportPackage { get; set; }

        /// <summary>
        /// Windows only. Make the Editor use Direct3D 11 for rendering. Normally the graphics API depends on Player settings(typically defaults to D3D11).
        /// </summary>
        public bool ForceD3D11 { get; set; }

        /// <summary>
        /// MacOS only. When using Metal, make the Editor use a particular GPU device by passing it the index of that GPU.
        /// </summary>
        public bool ForceDeviceIndex { get; set; }

        /// <summary>
        /// MacOS only. Make the Editor use Metal as the default graphics API.
        /// </summary>
        public bool ForceGfxMetal { get; set; }

        /// <summary>
        /// Make the Editor use OpenGL core profile for rendering.
        /// </summary>
        public ForceGLCore? ForceGLCore { get; set; }

        /// <summary>
        /// Windows only. Make the Editor use OpenGL for Embedded Systems for rendering.
        /// </summary>
        public ForceGLES? ForceGLES { get; set; }

        /// <summary>
        /// Used with -force-glcoreXY to prevent checking for additional OpenGL extensions, allowing it to run between platforms with the same code paths.
        /// </summary>
        public bool ForceClamped { get; set; }

        /// <summary>
        /// Make the Editor run as if there is a free Unity license on the machine, even if a Unity Pro license is installed.
        /// </summary>
        public bool ForceFree { get; set; }

        /// <summary>
        /// MacOS only. When using Metal, make the Editor use a low power device.
        /// </summary>
        public bool ForceLowPowerDevice { get; set; }

        /// <summary>
        /// Import the given package. No import dialog is shown.
        /// </summary>
        public FilePath ImportPackage { get; set; }

        /// <summary>
        /// Specify where the Editor or Windows/Linux/OSX standalone log file are written. If the path is ommitted, OSX and Linux will write output to the console. Windows uses the path %LOCALAPPDATA%\Unity\Editor\Editor.log as a default.
        /// </summary>
        public FilePath LogFile { get; set; }

        /// <summary>
        /// When running in batch mode, do not initialize the graphics device at all. This makes it possible to run your automated workflows on machines that don’t even have a GPU (automated workflows only work when you have a window in focus, otherwise you can’t send simulated input commands). Note that -nographics does not allow you to bake GI, because Enlighten requires GPU acceleration.
        /// </summary>
        public bool NoGraphics { get; set; }

        /// <summary>
        /// Disables the Unity Package Manager.
        /// </summary>
        public bool NoUPM { get; set; }

        /// <summary>
        /// Enter a password into the log-in form during activation of the Unity Editor.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Open the project at the given path.
        /// </summary>
        public DirectoryPath ProjectPath { get; set; }

        /// <summary>
        /// Quit the Unity Editor after other commands have finished executing. Note that this can cause error messages to be hidden (however, they still appear in the Editor.log file).
        /// </summary>
        public bool Quit { get; set; } = true;

        /// <summary>
        /// Return the currently active license to the license server. Please allow a few seconds before the license file is removed, because Unity needs to communicate with the license server.
        /// </summary>
        public bool ReturnLicense { get; set; }

        /// <summary>
        /// Run all your test by your TestPlatform;
        /// </summary>
        public bool RunTests { get; set; }

        /// <summary>
        /// Allows the selection of an active test platform.
        /// </summary>
        public TestPlatform? TestPlatform { get; set; }

        /// <summary>
        /// Activate Unity with the specified serial key. It is good practice to pass the -batchmode and -quit arguments as well, in order to quit Unity when done, if using this for automated activation of Unity. Please allow a few seconds before the license file is created, because Unity needs to communicate with the license server. Make sure that license file folder exists, and has appropriate permissions before running Unity with this argument. If activation fails, see the Editor.log for info.
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        /// Supported only on Android. Sets the default texture compression to the desired format before importing a texture or building the project. This is so you don’t have to import the texture again with the format you want.
        /// </summary>
        public DefaultPlatformTextureFormat? SetDefaultPlatformTextureFormat { get; set; }

        /// <summary>
        /// Prevent Unity from displaying the dialog that appears when a Standalone Player crashes. This argument is useful when you want to run the Player in automated builds or tests, where you don’t want a dialog prompt to obstruct automation.
        /// </summary>
        public bool SilentCrashes { get; set; }

        /// <summary>
        /// Enter a username into the log-in form during activation of the Unity Editor.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Specify a space-separated list of assembly names as parameters for Unity to ignore on automatic updates. Pass empty array to ignore all assemblies.
        /// </summary>
        public string[] DisableAssemblyUpdater { get; set; }

        /// <summary>
        /// Use this command line option to specify that APIUpdater should run when Unity is launched in batch mode. Omitting this command line argument when launching Unity in batch mode results in APIUpdater not running which can lead to compiler errors. Note that in versions prior to 2017.2 there’s no way to not run APIUpdater when Unity is launched in batch mode.
        /// </summary>
        public bool AcceptAPIUpdate { get; set; }

        /// <summary>
        /// <para>Custom arguments which can further be processed in Unity Editor script by calling System.Environment.GetCommandLineArgs method. </para>
        /// <para>They are supplied among other arguments in format "--key=value". </para>
        /// <para>Expected to be used in conjunction with ExecuteMethod argument. </para>
        /// </summary>
        /// <example>
        /// <code>
        /// var arguments = new UnityEditorArguments { ExecuteMethod = "Build.RunCommandLineBuild" };
        /// arguments.Custom.buildNumber = 42;
        /// arguments.Custom.packageTitle = "Game of Builds";
        /// </code>
        /// </example>
        public dynamic Custom => customArguments;

        /// <summary>
        /// <para>Custom arguments which can further be processed in Unity Editor script by calling System.Environment.GetCommandLineArgs method. </para>
        /// <para>They are supplied among other arguments in format "--key=value". </para>
        /// <para>Expected to be used in conjunction with ExecuteMethod argument. </para>
        /// </summary>
        /// <example>
        /// <code>
        /// new UnityEditorArguments
        /// {
        ///     ExecuteMethod = "Build.RunCommandLineBuild",
        ///     SetCustomArguments = x =>
        ///     {
        ///         x.buildNumber = 42;
        ///         x.packageTitle = "Game of Builds";
        ///     },
        /// };
        /// </code>
        /// </example>
        public Action<dynamic> SetCustomArguments { set => value.Invoke(customArguments); }

        internal ProcessArgumentBuilder CustomizeCommandLineArguments(ProcessArgumentBuilder builder, ICakeEnvironment environment)
        {
            if (Quit && RunTests)
                throw new Exception(
                    $"Unable to start unit testing if argument {nameof(Quit)} is true.\nPlease set {nameof(Quit)} or {nameof(RunTests)} to false");

            if (AssetServerUpdate != null)
            {
                builder.Append("-assetServerUpdate");
                if (AssetServerUpdate.Port.HasValue)
                    builder.Append(AssetServerUpdate.IP + ":" + AssetServerUpdate.Port.Value.ToString());
                else
                    builder.Append(AssetServerUpdate.IP);
                builder.Append(AssetServerUpdate.ProjectName);
                builder.Append(AssetServerUpdate.Username);
                builder.Append(AssetServerUpdate.Password);
                if (AssetServerUpdate.Revision != null)
                {
                    builder.Append("r");
                    builder.Append(AssetServerUpdate.Revision);
                }
            }

            if (BatchMode)
                builder
                    .Append("-batchmode");

            if (BuildLinux32Player != null)
                builder
                    .Append("-buildLinux32Player")
                    .AppendQuoted(BuildLinux32Player.MakeAbsolute(environment).FullPath);

            if (BuildLinux64Player != null)
                builder
                    .Append("-buildLinux64Player")
                    .AppendQuoted(BuildLinux64Player.MakeAbsolute(environment).FullPath);

            if (BuildLinuxUniversalPlayer != null)
                builder
                    .Append("-buildLinuxUniversalPlayer")
                    .AppendQuoted(BuildLinuxUniversalPlayer.MakeAbsolute(environment).FullPath);

            if (BuildOSXPlayer != null)
                builder
                    .Append("-buildOSXPlayer")
                    .AppendQuoted(BuildOSXPlayer.MakeAbsolute(environment).FullPath);

            if (BuildOSX64Player != null)
                builder
                    .Append("-buildOSX64Player")
                    .AppendQuoted(BuildOSX64Player.MakeAbsolute(environment).FullPath);

            if (BuildOSXUniversalPlayer != null)
                builder
                    .Append("-buildOSXUniversalPlayer")
                    .AppendQuoted(BuildOSXUniversalPlayer.MakeAbsolute(environment).FullPath);

            if (BuildTarget.HasValue)
                builder
                    .Append("-buildTarget")
                    .Append(BuildTarget.Value.ToString());

            if (CustomBuildTarget != null)
            {
                builder
                    .Append("-buildTarget")
                    .Append(CustomBuildTarget);
            }

            if (CustomBuildTarget != null && BuildTarget.HasValue)
            {
                throw new ArgumentException("Providing both BuildTarget and CustomBuildTarget is not supported.");
            }

            if (BuildWindowsPlayer != null)
                builder
                    .Append("-buildWindowsPlayer")
                    .AppendQuoted(BuildWindowsPlayer.MakeAbsolute(environment).FullPath);

            if (BuildWindows64Player != null)
                builder
                    .Append("-buildWindows64Player")
                    .AppendQuoted(BuildWindows64Player.MakeAbsolute(environment).FullPath);

            if (StackTraceLogType.HasValue)
                builder
                    .Append("-stackTraceLogType")
                    .Append(StackTraceLogType.Value.ToString());

            if (CacheServerIPAddress != null)
                builder
                    .Append("-CacheServerIPAddress")
                    .Append(CacheServerIPAddress);

            if (CreateProject != null)
                builder
                    .Append("-createProject")
                    .AppendQuoted(CreateProject.MakeAbsolute(environment).FullPath);

            if (EditorTestsCategories != null)
                builder
                    .Append("-editorTestsCategories")
                    .Append(EditorTestsCategories);

            if (EditorTestsFilter != null)
                builder
                    .Append("-editorTestsFilter")
                    .Append(EditorTestsFilter);

            if (TestResults != null)
                builder
                    .Append("-testResults")
                    .AppendQuoted(TestResults.MakeAbsolute(environment).FullPath);

            if (ExecuteMethod != null)
                builder
                    .Append("-executeMethod")
                    .Append(ExecuteMethod);

            if (ExportPackage != null && ExportPackage.AssetPaths != null && ExportPackage.PackageName != null)
            {
                builder.Append("-exportPackage");
                foreach (var exportPackageAssetPath in ExportPackage.AssetPaths)
                    builder.AppendQuoted(exportPackageAssetPath);
                builder.AppendQuoted(ExportPackage.PackageName);
            }

            if (ForceD3D11)
                builder.Append("-force-d3d11");

            if (ForceDeviceIndex)
                builder.Append("-force-device-index");

            if (ForceGfxMetal)
                builder.Append("-force-gfx-metal");

            if (ForceGLCore.HasValue)
                builder.Append("-force-glcore" + ForceGLCore.Value.Render());

            if (ForceGLES.HasValue)
                builder.Append("-force-gles" + ForceGLES.Value.Render());

            if (ForceClamped)
                builder.Append("-force-clamped");

            if (ForceFree)
                builder.Append("-force-free");

            if (ForceLowPowerDevice)
                builder.Append("-force-low-power-device");

            if (ImportPackage != null)
                builder
                    .Append("-importPackage")
                    .AppendQuoted(ImportPackage.MakeAbsolute(environment).FullPath);

            if (LogFile != null)
                builder
                    .Append("-logFile")
                    .AppendQuoted(LogFile.FullPath);

            if (NoGraphics)
                builder.Append("-nographics");

            if (NoUPM)
                builder.Append("-noUpm");

            if (Password != null)
                builder
                    .Append("-password")
                    .Append(Password); //may be use AppendQuoted instead??

            if (ProjectPath != null)
                builder
                    .Append("-projectPath")
                    .AppendQuoted(ProjectPath.MakeAbsolute(environment).FullPath);

            if (Quit)
                builder
                    .Append("-quit");

            if (ReturnLicense)
                builder.Append("-returnlicense");

            if (RunTests && ProjectPath != null)
                builder.Append("-runTests");

            if (RunTests && TestPlatform.HasValue)
                builder
                    .Append("-testPlatform")
                    .Append(TestPlatform.Value.ToString());

            if (Serial != null)
                builder
                    .Append("-serial")
                    .Append(Serial);

            if (SetDefaultPlatformTextureFormat.HasValue)
                builder
                    .Append("-setDefaultPlatformTextureFormat")
                    .Append(SetDefaultPlatformTextureFormat.Value.ToString());

            if (SilentCrashes)
                builder.Append("-silent-crashes");

            if (Username != null)
                builder
                    .Append("-username")
                    .Append(Username); //may be use AppendQuoted instead??

            if (DisableAssemblyUpdater != null)
            {
                builder.Append("-disable-assembly-updater");
                foreach (var assembly in DisableAssemblyUpdater)
                    builder.AppendQuoted(assembly);
            }

            if (AcceptAPIUpdate)
                builder.Append("-accept-apiupdate");

            foreach (var customArgument in customArguments)
                builder.AppendQuoted($"--{customArgument.Key}={customArgument.Value}");

            return builder;
        }
    }
}
