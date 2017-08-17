using Cake.Core;
using Cake.Core.IO;

namespace Cake.Unity.Actions
{
    /// <summary>
    /// Execute the static method as soon as Unity is started,
    /// the project is open and after the optional Asset server update has been performed.
    /// This can be used to do tasks such as continous integration,
    /// performing Unit Tests, making builds or preparing data. 
    /// </summary>
    public class UnityExecuteMethodAction : UnityAction
    {
        private readonly string _method;

        public UnityExecuteMethodAction(string method)
        {
            _method = method;
        }

        public override void BuildArguments(ICakeContext context, ProcessArgumentBuilder arguments)
        {
            arguments.Append("-executeMethod");
            arguments.Append(_method);
        }
    }
}
