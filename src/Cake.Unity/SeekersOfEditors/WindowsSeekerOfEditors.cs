using System.Diagnostics;
using System.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Unity.Version;

namespace Cake.Unity.SeekersOfEditors
{
    internal class WindowsSeekerOfEditors : SeekerOfEditors
    {
        public WindowsSeekerOfEditors(ICakeEnvironment environment, IGlobber globber, ICakeLog log)
            : base(environment, globber, log) { }

        private string ProgramFiles => environment.GetSpecialPath(SpecialPath.ProgramFiles).FullPath;

        protected override string[] SearchPatterns => new []
        {
            $"{ProgramFiles}/*Unity*/*/Editor/Unity.exe",
            $"{ProgramFiles}/*Unity*/*/*/Editor/Unity.exe",
            $"{ProgramFiles}/*Unity*/*/*/*/Editor/Unity.exe"
        };

        protected override UnityVersion DetermineVersion(FilePath editorPath)
        {
            log.Debug("Determining version of Unity Editor at path {0}...", editorPath);

            var fileVersion = FileVersionInfo.GetVersionInfo(editorPath.FullPath);

            var (year, stream, update) = (fileVersion.FileMajorPart, fileVersion.FileMinorPart, fileVersion.FileBuildPart);

            if (year <= 0 || stream <= 0 || update < 0)
            {
                log.Warning("Unity Editor file version {0} is incorrect.", $"{year}.{stream}.{update}.{fileVersion.FilePrivatePart}");
                log.Warning("Expected first two numbers to be positive and third one to be non negative.");
                log.Warning("Path: {0}", editorPath.FullPath);
                log.Warning(string.Empty);
                return null;
            }

            log.Debug("Unity version from file version: {0}.{1}.{2}", year, stream, update);

            var suffix = DetermineVersionSuffix(editorPath, year, stream, update);

            if (suffix.HasValue)
            {
                var version = new UnityVersion(year, stream, update, suffix.Value.character, suffix.Value.number);
                log.Debug("Result Unity Editor version (full): {0}", version);
                log.Debug(string.Empty);
                return version;
            }
            else
            {
                var version = new UnityVersion(year, stream, update);
                log.Debug("Result Unity Editor version (short): {0}", version);
                log.Debug(string.Empty);
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
