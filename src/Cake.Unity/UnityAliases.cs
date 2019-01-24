using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Unity.Platforms;
using Cake.Unity.Version;
using static Cake.Core.PlatformFamily;
using static Cake.Unity.Version.UnityReleaseStage;

namespace Cake.Unity
{
    [CakeAliasCategory("Unity")]
    public static class UnityAliases
    {
        private static IReadOnlyCollection<UnityEditorDescriptor> unityEditorsCache;

        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.Unity.Platforms")]
        public static void UnityBuild(this ICakeContext context, DirectoryPath projectPath, UnityPlatform platform)
        {
            var tool = new UnityRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            tool.Run(context, projectPath, platform);
        }



        /// <summary>
        /// Executes Unity Editor via command-line interface.
        /// </summary>
        /// <param name="unityEditorPath">Path to Unity Editor executable.</param>
        /// <param name="arguments">Unity Editor command-line arguments.</param>
        /// <example>
        /// <code>
        /// var unityEditor = FindUnityEditor(2018, 3) ?? throw new Exception("Cannot find Unity Editor 2018.3.");
        /// 
        /// UnityEditor(unityEditor.Path, new UnityEditorArguments
        /// {
        ///     ProjectPath = "A:/UnityProject",
        ///     BuildWindowsPlayer = "A:/Build/game.exe",
        ///     LogFile = "A:/Build/unity.log",
        /// });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static void UnityEditor(this ICakeContext context,
            FilePath unityEditorPath, UnityEditorArguments arguments) =>
            UnityEditor(context, unityEditorPath, arguments, new UnityEditorSettings());

        /// <summary>
        /// Executes Unity Editor via command-line interface.
        /// </summary>
        /// <param name="unityEditorPath">Path to Unity Editor executable.</param>
        /// <param name="arguments">Unity Editor command-line arguments.</param>
        /// <param name="settings">Settings which affect how Unity Editor should be executed.</param>
        /// <example>
        /// <code>
        /// var unityEditor = FindUnityEditor(2018, 3) ?? throw new Exception("Cannot find Unity Editor 2018.3.");
        /// 
        /// UnityEditor(
        ///     unityEditor.Path,
        ///     new UnityEditorArguments
        ///     {
        ///         ProjectPath = "A:/UnityProject",
        ///         BuildWindowsPlayer = "A:/Build/game.exe",
        ///         LogFile = "A:/Build/unity.log",
        ///     },
        ///     new UnityEditorSettings
        ///     {
        ///         RealTimeLog = true,
        ///     });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static void UnityEditor(this ICakeContext context,
            FilePath unityEditorPath, UnityEditorArguments arguments, UnityEditorSettings settings) =>
            new UnityEditor(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log)
                .Run(unityEditorPath, arguments, settings);

        /// <summary>
        /// Executes Unity Editor via command-line interface.
        /// </summary>
        /// <param name="unityEditor">Unity Editor descriptor provided by a FindUnityEditor method.</param>
        /// <param name="arguments">Unity Editor command-line arguments.</param>
        /// <param name="settings">Optional settings which affect how Unity Editor should be executed.</param>
        /// <example>
        /// <code>
        /// UnityEditor(
        ///     FindUnityEditor(2018, 3) ?? throw new Exception("Cannot find Unity Editor 2018.3."),
        ///     new UnityEditorArguments
        ///     {
        ///         ProjectPath = "A:/UnityProject",
        ///         BuildWindowsPlayer = "A:/Build/game.exe",
        ///         LogFile = "A:/Build/unity.log",
        ///     },
        ///     new UnityEditorSettings
        ///     {
        ///         RealTimeLog = true,
        ///     });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static void UnityEditor(this ICakeContext context,
            UnityEditorDescriptor unityEditor, UnityEditorArguments arguments, UnityEditorSettings settings = null) =>
            new UnityEditor(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log)
                .Run(unityEditor, arguments, settings ?? new UnityEditorSettings());

        /// <summary>
        /// <para>Executes Unity Editor via command-line interface.</para>
        /// <para>Determines Unity Editor location automatically by specified version.</para>
        /// </summary>
        /// <param name="versionYear">Year part of Unity version aka major version.</param>
        /// <param name="versionStream">Stream part of Unity version aka minor version.</param>
        /// <param name="arguments">Unity Editor command-line arguments.</param>
        /// <param name="settings">Optional settings which affect how Unity Editor should be executed.</param>
        /// <example>
        /// <code>
        /// UnityEditor(
        ///     2018, 3,
        ///     new UnityEditorArguments
        ///     {
        ///         ProjectPath = "A:/UnityProject",
        ///         BuildWindowsPlayer = "A:/Build/game.exe",
        ///         LogFile = "A:/Build/unity.log",
        ///     },
        ///     new UnityEditorSettings
        ///     {
        ///         RealTimeLog = true,
        ///     });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static void UnityEditor(this ICakeContext context,
            int versionYear, int versionStream, UnityEditorArguments arguments, UnityEditorSettings settings = null) =>
            new UnityEditor(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log)
                .Run(
                    context.FindUnityEditor(versionYear, versionStream)
                    ?? throw new Exception($"Failed to locate Unity Editor {versionYear}.{versionStream}. Try to specify it's path explicitly."),
                    arguments,
                    settings ?? new UnityEditorSettings());

        /// <summary>
        /// <para>Executes Unity Editor via command-line interface.</para>
        /// <para>Determines Unity Editor location automatically by specified version.</para>
        /// </summary>
        /// <param name="versionYear">Year part of Unity version aka major version.</param>
        /// <param name="arguments">Unity Editor command-line arguments.</param>
        /// <param name="settings">Optional settings which affect how Unity Editor should be executed.</param>
        /// <example>
        /// <code>
        /// UnityEditor(
        ///     2018,
        ///     new UnityEditorArguments
        ///     {
        ///         ProjectPath = "A:/UnityProject",
        ///         BuildWindowsPlayer = "A:/Build/game.exe",
        ///         LogFile = "A:/Build/unity.log",
        ///     },
        ///     new UnityEditorSettings
        ///     {
        ///         RealTimeLog = true,
        ///     });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static void UnityEditor(this ICakeContext context,
            int versionYear, UnityEditorArguments arguments, UnityEditorSettings settings = null) =>
            new UnityEditor(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log)
                .Run(
                    context.FindUnityEditor(versionYear)
                    ?? throw new Exception($"Failed to locate Unity Editor {versionYear}. Try to specify it's path explicitly."),
                    arguments,
                    settings ?? new UnityEditorSettings());

        /// <summary>
        /// <para>Executes Unity Editor via command-line interface.</para>
        /// <para>Determines location of latest available Unity Editor automatically.</para>
        /// </summary>
        /// <param name="arguments">Unity Editor command-line arguments.</param>
        /// <param name="settings">Optional settings which affect how Unity Editor should be executed.</param>
        /// <example>
        /// <code>
        /// UnityEditor(
        ///     new UnityEditorArguments
        ///     {
        ///         ProjectPath = "A:/UnityProject",
        ///         BuildWindowsPlayer = "A:/Build/game.exe",
        ///         LogFile = "A:/Build/unity.log",
        ///     },
        ///     new UnityEditorSettings
        ///     {
        ///         RealTimeLog = true,
        ///     });
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static void UnityEditor(this ICakeContext context,
            UnityEditorArguments arguments, UnityEditorSettings settings = null) =>
            new UnityEditor(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools, context.Log)
                .Run(
                    context.FindUnityEditor()
                    ?? throw new Exception("Failed to locate Unity Editor. Try to specify it's path explicitly."),
                    arguments,
                    settings ?? new UnityEditorSettings());



        /// <summary>
        /// Locates installed Unity Editor with latest version.
        /// </summary>
        /// <returns>Descriptor of Unity Editor or null.</returns>
        /// <exception cref="NotSupportedException">Thrown for non-Windows platforms.</exception>
        /// <example>
        /// <code>
        /// var editor = FindUnityEditor();
        /// if (editor != null)
        ///     Information("Found Unity Editor {0} at path {1}", editor.Version, editor.Path);
        /// else
        ///     Warning("Cannot find Unity Editor");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.Unity.Version")]
        public static UnityEditorDescriptor FindUnityEditor(this ICakeContext context) =>
            Enumerable.FirstOrDefault
            (
                from editor in context.FindUnityEditors()
                let version = editor.Version
                orderby
                    ReleaseStagePriority(version.Stage),
                    version.Year descending,
                    version.Stream descending,
                    version.Update descending
                select editor
            );

        /// <summary>
        /// <para>Locates installed Unity Editor by version (year).</para>
        /// <para>If more than one Unity Editor satisfies specified version, then latest one is returned.</para>
        /// </summary>
        /// <param name="year">Year part of Unity version (aka major version).</param>
        /// <returns>Descriptor of Unity Editor or null.</returns>
        /// <exception cref="NotSupportedException">Thrown for non-Windows platforms.</exception>
        /// <example>
        /// <code>
        /// var editor = FindUnityEditor(2018);
        /// if (editor != null)
        ///     Information("Found Unity Editor {0} at path {1}", editor.Version, editor.Path);
        /// else
        ///     Warning("Cannot find Unity Editor 2018");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.Unity.Version")]
        public static UnityEditorDescriptor FindUnityEditor(this ICakeContext context, int year) =>
            Enumerable.FirstOrDefault
            (
                from editor in context.FindUnityEditors()
                let version = editor.Version
                where
                    version.Year == year
                orderby
                    ReleaseStagePriority(version.Stage),
                    version.Stream descending,
                    version.Update descending
                select editor
            );

        /// <summary>
        /// <para>Locates installed Unity Editor by version (year and stream).</para>
        /// <para>If more than one Unity Editor satisfies specified version, then latest one is returned.</para>
        /// </summary>
        /// <param name="year">Year part of Unity version aka major version.</param>
        /// <param name="stream">Stream part of Unity version (aka minor version). Usually 1, 2 and 3 mean tech stream while 4 is long term support.</param>
        /// <returns>Descriptor of Unity Editor or null.</returns>
        /// <exception cref="NotSupportedException">Thrown for non-Windows platforms.</exception>
        /// <example>
        /// <code>
        /// var editor = FindUnityEditor(2018, 3);
        /// if (editor != null)
        ///     Information("Found Unity Editor {0} at path {1}", editor.Version, editor.Path);
        /// else
        ///     Warning("Cannot find Unity Editor 2018.3");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.Unity.Version")]
        public static UnityEditorDescriptor FindUnityEditor(this ICakeContext context, int year, int stream) =>
            Enumerable.FirstOrDefault
            (
                from editor in context.FindUnityEditors()
                let version = editor.Version
                where
                    version.Year == year &&
                    version.Stream == stream
                orderby
                    ReleaseStagePriority(version.Stage),
                    version.Update descending
                select editor
            );

        /// <summary>
        /// Locates installed Unity Editors.
        /// </summary>
        /// <returns>Collection of found Unity Editors (path and version info).</returns>
        /// <exception cref="NotSupportedException">Thrown for non-Windows platforms.</exception>
        /// <example>
        /// <code>
        /// foreach (var editor in FindUnityEditors())
        ///     Information("Found Unity Editor {0} at path {1}", editor.Version, editor.Path);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.Unity.Version")]
        public static IReadOnlyCollection<UnityEditorDescriptor> FindUnityEditors(this ICakeContext context)
        {
            if (context.Environment.Platform.Family != Windows)
                throw new NotSupportedException("Cannot locate Unity Editors. Only Windows platform is supported.");

            if (unityEditorsCache != null)
            {
                context.Log.Debug("Already searched for Unity Editors. Using cached results.");
                return unityEditorsCache;
            }

            return
                unityEditorsCache =
                    new SeekerOfEditors(context.Environment, context.Globber, context.Log)
                        .Seek();
        }

        private static int ReleaseStagePriority(UnityReleaseStage stage)
        {
            switch (stage)
            {
                case Final:
                case Patch:
                    return 1;

                case Beta:
                    return 3;

                case Alpha:
                    return 4;

                default:
                    return 2;
            }
        }
    }
}
