using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Unity.Version;

namespace Cake.Unity
{
    internal class SeekerOfEditors
    {
        private readonly ICakeEnvironment environment;
        private readonly IGlobber globber;
        private readonly ICakeLog log;

        public SeekerOfEditors(ICakeEnvironment environment, IGlobber globber, ICakeLog log)
        {
            this.environment = environment;
            this.globber = globber;
            this.log = log;
        }

        private string ProgramFiles => environment.GetSpecialPath(SpecialPath.ProgramFiles).FullPath;

        public IReadOnlyCollection<UnityEditorDescriptor> Seek()
        {
            var searchPattern = $"{ProgramFiles}/*Unity*/**/Editor/Unity.exe";

            log.Debug("Searching for available Unity Editors...");
            log.Debug("Search pattern: {0}", searchPattern);
            var candidates = globber.GetFiles(searchPattern).ToList();

            log.Debug("Found {0} candidates.", candidates.Count);

            var editors =
                from candidatePath in candidates
                let version = DetermineVersion(candidatePath)
                where version != null
                select new UnityEditorDescriptor(version, candidatePath);

            return editors.ToList();
        }

        private UnityVersion DetermineVersion(FilePath editorPath)
        {
            log.Debug(string.Empty);
            log.Debug("Determining version of Unity Editor at path {0}...", editorPath);

            var fileVersion = FileVersionInfo.GetVersionInfo(editorPath.FullPath);

            var (year, stream, update) = (fileVersion.FileMajorPart, fileVersion.FileMinorPart, fileVersion.FileBuildPart);

            if (year <= 0 || stream <= 0 || update < 0)
            {
                log.Warning(
                    "Unity Editor file version {0} at path {1} is incorrect. Expected first two parts to be positive numbers and third one to be non negative.",
                    $"{year}.{stream}.{update}.{fileVersion.FilePrivatePart}",
                    editorPath.FullPath);
                return null;
            }

            log.Debug("Unity version from file version: {0}.{1}.{2}", year, stream, update);

            var suffix = DetermineVersionSuffix(editorPath, year, stream, update);

            if (suffix.HasValue)
            {
                var version = new UnityVersion(year, stream, update, suffix.Value.character, suffix.Value.number);
                log.Debug("Result Unity Editor version (full): {0}", version);
                return version;
            }
            else
            {
                var version = new UnityVersion(year, stream, update);
                log.Debug("Result Unity Editor version (short): {0}", version);
                return version;
            }
        }

        private (char character, int number)? DetermineVersionSuffix(FilePath editorPath, int year, int stream, int update)
        {
            log.Debug("Determining version suffix from folder structure...");

            var parentFolders =
                editorPath
                    .GetDirectory()
                    .Segments
                    .Reverse();

            var suffixes =
                Enumerable.ToList
                (
                    from folder in parentFolders
                    where folder.StartsWith($"{year}.{stream}.{update}")
                    let suffixString = folder.Remove(0, $"{year}.{stream}.{update}".Length)
                    let suffixCharacter = suffixString[0]
                    let suffixNumberPart = suffixString.Remove(0, 1)
                    where IsInt(suffixNumberPart)
                    let suffixNumber = int.Parse(suffixNumberPart)
                    select (character: suffixCharacter, number: suffixNumber)
                );

            if (suffixes.Count == 0)
            {
                log.Debug("Failed: no suitable folders found. Try to install needed Editor using Unity Hub.");
                return null;
            }

            var suffix = suffixes.First();
            log.Debug("Determined suffix: {0}{1}", suffix.character, suffix.number);

            return suffix;
        }

        private bool IsInt(string value) => int.TryParse(value, out _);
    }
}
