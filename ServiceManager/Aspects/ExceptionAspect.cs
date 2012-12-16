using System;
using PostSharp.Aspects;
using Rhyous.ServiceManager.Singletons;
using PostSharp.Aspects.Dependencies;

namespace Rhyous.ServiceManager.Aspects
{
    [Serializable]
    [AspectTypeDependency(AspectDependencyAction.Commute, typeof(ExceptionAspect))]
    [ProvideAspectRole(StandardRoles.ExceptionHandling)]
    public class ExceptionAspect : OnExceptionAspect
    {
        public String Message { get; set; }

        public Type ExceptionType { get; set; }

        public override void OnException(MethodExecutionArgs args)
        {
            string msg = DateTime.Now + ": " + Message + Environment.NewLine;
            msg += string.Format("{0}: Error running {1}. {2}", DateTime.Now, args.Method.Name, args.Exception.Message);
            msg += Environment.NewLine;
            msg += args.Exception.StackTrace;
            Log.WriteLine(msg);
            args.FlowBehavior = FlowBehavior.Continue;
        }

        public override Type GetExceptionType(System.Reflection.MethodBase targetMethod)
        {
            return ExceptionType;
        }
    }
}
