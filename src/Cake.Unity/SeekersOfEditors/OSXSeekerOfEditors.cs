using System.IO;
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

        protected override string SearchPattern => "/Applications/**/Unity.app/Contents/MacOS/Unity";

        protected override UnityVersion DetermineVersion(FilePath editorPath)
        {
            log.Debug($"Determining version of Unity Editor at path {editorPath}...");

            var plistPath = editorPath.FullPath.Replace("MacOS/Unity", "Info.plist");
            var lines = File.ReadAllLines(plistPath);
            for (int i = 0; i < lines.Length; ++i)
            {
                if (lines[i].Contains("<key>CFBundleVersion</key>"))
                {
                    ++i;
                    int pos1 = lines[i].IndexOf('>');
                    int pos2 = lines[i].LastIndexOf('<');
                    var version =
                        lines[i].Substring(pos1 + 1, pos2 - pos1 - 1)
                            .Split('.');
                    int year = int.Parse(version[0]);
                    int stream = int.Parse(version[1]);
                    int charPos = FirstNotDigit(version[2]);
                    int update = int.Parse(version[2].Substring(0, charPos));
                    char suffixCharacter = version[2][charPos];
                    int suffixNumber = int.Parse(version[2].Substring(charPos + 1));

                    var unityVersion = new UnityVersion(year, stream, update, suffixCharacter, suffixNumber);
                    log.Debug($"Result Unity Editor version (full): {unityVersion}");
                    return unityVersion;
                }
            }
            return null;
        }

        private int FirstNotDigit(string str)
        {
            for (int i = 0; i < str.Length; ++i)
                if (!char.IsDigit(str[i]))
                    return i;
            return -1;
        }
    }
}
