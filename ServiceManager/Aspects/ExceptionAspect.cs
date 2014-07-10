using System;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;
using Rhyous.ServiceManager.Singletons;

namespace Rhyous.ServiceManager.Aspects
{
    [Serializable]
    [AspectTypeDependency(AspectDependencyAction.Commute, typeof(ExceptionAspect))]
    [ProvideAspectRole(StandardRoles.ExceptionHandling)]
    public class ExceptionAspect : OnExceptionAspect
    {
        public string Message { get; set; }

        public Type ExceptionType { get; set; }

        public override void OnException(MethodExecutionArgs args)
        {
            var msg = DateTime.Now + ": " + Message + Environment.NewLine;
            msg += string.Format("{0}: Error running {1}. {2}", DateTime.Now, args.Method.Name, args.Exception.Message);
            msg += Environment.NewLine;
            msg += args.Exception.StackTrace;
            Log.WriteLine(msg);
            args.FlowBehavior = FlowBehavior.Continue;
        }

        public override Type GetExceptionType(MethodBase targetMethod)
        {
            return ExceptionType;
        }
    }
}
