using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Unity.Platforms;
using static Cake.Core.PlatformFamily;

namespace Cake.Unity
{
    [CakeAliasCategory("Unity")]
    public static class UnityExtensions
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

        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static UnityEditorDescriptor FindUnityEditor(this ICakeContext context) =>
            Enumerable.FirstOrDefault
            (
                from editor in context.FindUnityEditors()
                let version = editor.Version
                orderby
                    version.Year descending,
                    version.Stream descending,
                    version.Update descending
                select editor
            );

        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static UnityEditorDescriptor FindUnityEditor(this ICakeContext context, int year) =>
            Enumerable.FirstOrDefault
            (
                from editor in context.FindUnityEditors()
                let version = editor.Version
                where
                    version.Year == year
                orderby
                    version.Stream descending,
                    version.Update descending
                select editor
            );

        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static UnityEditorDescriptor FindUnityEditor(this ICakeContext context, int year, int stream) =>
            Enumerable.FirstOrDefault
            (
                from editor in context.FindUnityEditors()
                let version = editor.Version
                where
                    version.Year == year &&
                    version.Stream == stream
                orderby
                    version.Update descending
                select editor
            );

        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
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
    }
}
