using System;
using PostSharp.Aspects;
using Rhyous.ServiceManager.Singletons;
using PostSharp.Aspects.Dependencies;

namespace Common.Aspects
{
    [Serializable]
    [ProvideAspectRole(StandardRoles.Tracing)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.DataBinding)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.ExceptionHandling)]  
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.Persistence)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.Threading)]    
    public class MethodTraceAspect : OnMethodBoundaryAspect
    {
        private static int _TabCount;

        public override void OnEntry(MethodExecutionArgs args)
        {
            Log.WriteLine(GetTabs() + "Method started: " + args.Method.Name);
            _TabCount++;
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _TabCount--;
            Log.WriteLine(GetTabs() + "Method completed: " + args.Method.Name);
        }

        private static String GetTabs()
        {
            var tabs = string.Empty;
            for (var i = 0; i < _TabCount; i++)
            {
                tabs += "\t";
            }
            return tabs;
        }
    }
}