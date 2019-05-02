using System.Linq;
using System.Xml.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Unity.Version;

namespace Cake.Unity.SeekersOfEditors
{
    internal class OSXSeekerOfEditors : SeekerOfEditors
    {
        public OSXSeekerOfEditors(ICakeEnvironment environment, IGlobber globber, ICakeLog log)
            : base(environment, globber, log)
        { }

        protected override string SearchPattern => "/Applications/**/Unity*.app/Contents/MacOS/Unity";

        protected override UnityVersion DetermineVersion(FilePath editorPath)
        {
            log.Debug($"Determining version of Unity Editor at path {editorPath}...");

            var version = UnityVersionFromInfoPlist(editorPath);

            if (version == null)
            {
                log.Debug($"Can't find UnityVersion for {editorPath}");
                return null;
            }

            var unityVersion = UnityVersion.Parse(version);

            log.Debug($"Result Unity Editor version (full): {unityVersion}");
            return unityVersion;
        }

        private static string UnityVersionFromInfoPlist(FilePath editorPath) =>
            XElement
                .Load(PlistPath(editorPath))
                .Descendants()
                .SkipWhile((element) => element.Value != "CFBundleVersion")
                .Skip(1)
                .FirstOrDefault()
                ?.Value;

        private static string PlistPath(FilePath editorPath) =>
            editorPath.FullPath.Replace("/MacOS/Unity", "/Info.plist");
    }
}
