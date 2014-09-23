using Cake.Core;
using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Unity
{
    public interface IUnityPlatform
    {
        void BuildArguments(ICakeContext context, ProcessArgumentBuilder builder);
    }
}
