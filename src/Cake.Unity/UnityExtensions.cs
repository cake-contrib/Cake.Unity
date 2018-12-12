using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Unity.Platforms;

namespace Cake.Unity
{
    [CakeAliasCategory("Unity")]
    public static class UnityExtensions
    {
        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        [CakeNamespaceImport("Cake.Unity.Platforms")]
        public static void UnityBuild(this ICakeContext context, DirectoryPath projectPath, UnityPlatform platform)
        {
            var tool = new UnityRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            tool.Run(context, projectPath, platform);
        }

        private static string ProgramFiles => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        [CakeMethodAlias]
        [CakeAliasCategory("Build")]
        public static IEnumerable<UnityEditorDescriptor> FindUnityEditors(this ICakeContext context)
        {
            string searchPattern = $"{ProgramFiles}/*Unity*/**/Editor/Unity.exe";

            context.Log.Debug("Searching for available Unity Editors...");
            context.Log.Debug("Search pattern: {0}", searchPattern);
            IEnumerable<FilePath> candidates = context.Globber.GetFiles(searchPattern);

            return
                from candidatePath in candidates
                let version = DetermineUnityEditorVersion(context, candidatePath)
                where version != null
                select new UnityEditorDescriptor(version, candidatePath);
        }

        private static string DetermineUnityEditorVersion(ICakeContext context, FilePath editorPath)
        {
            context.Log.Debug("Determining version of Unity Editor at path {0}...", editorPath);

            var fileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(editorPath.FullPath);

            var (year, stream, update) = (fileVersion.FileMajorPart, fileVersion.FileMinorPart, fileVersion.FileBuildPart);

            if (year <= 0 || stream <= 0 || update < 0)
            {
                context.Log.Debug("Failed: file version {0} is incorrect. Expected first two parts to be positive numbers and third one to be non negative.", fileVersion);
                return null;
            }

            string version = $"{year}.{stream}.{update}";

            context.Log.Debug("Success: {0}", version);

            return version;
        }
    }
}
