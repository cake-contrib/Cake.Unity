using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Unity.Version;

namespace Cake.Unity.SeekersOfEditors
{
    internal abstract class SeekerOfEditors
    {
        protected readonly ICakeEnvironment environment;
        private readonly IGlobber globber;
        protected readonly ICakeLog log;

        public static SeekerOfEditors GetSeeker(ICakeEnvironment environment, IGlobber globber, ICakeLog log)
        {
            if (environment.Platform.Family == PlatformFamily.Windows)
                return new WindowsSeekerOfEditors(environment, globber, log);

            if (environment.Platform.Family == PlatformFamily.OSX)
                return new OSXSeekerOfEditors(environment, globber, log);

            throw new NotSupportedException("Cannot locate Unity Editors. Only Windows and OSX platform is supported.");
        }

        protected SeekerOfEditors(ICakeEnvironment environment, IGlobber globber, ICakeLog log)
        {
            this.environment = environment;
            this.globber = globber;
            this.log = log;
        }

        public IReadOnlyCollection<UnityEditorDescriptor> Seek()
        {
            var searchPattern = SearchPattern;

            log.Debug("Searching for available Unity Editors...");
            log.Debug("Search pattern: {0}", searchPattern);
            var candidates = globber.GetFiles(searchPattern).ToList();

            log.Debug("Found {0} candidates.", candidates.Count);
            log.Debug(string.Empty);

            var editors =
                from candidatePath in candidates
                let version = DetermineVersion(candidatePath)
                where version != null
                select new UnityEditorDescriptor(version, candidatePath);

            return editors.ToList();
        }

        protected abstract string SearchPattern { get; }

        protected abstract UnityVersion DetermineVersion(FilePath editorPath);
    }
}
