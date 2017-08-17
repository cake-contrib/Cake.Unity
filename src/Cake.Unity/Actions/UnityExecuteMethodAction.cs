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
        private readonly string[] _extraParameters;

        public UnityExecuteMethodAction(string method, params string[] extraParameters)
        {
            _method = method;
            _extraParameters = extraParameters;
        }

        public override void BuildArguments(ICakeContext context, ProcessArgumentBuilder arguments)
        {
            base.BuildArguments(context, arguments);
            arguments.Append("-executeMethod");
            arguments.Append(_method);
            foreach (var extraParameter in _extraParameters)
            {
                arguments.Append(extraParameter);
            }
        }
    }
}
