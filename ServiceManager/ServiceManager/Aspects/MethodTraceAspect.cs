using System;
using PostSharp.Aspects;
using Rhyous.ServiceManager.Singletons;

namespace Common.Aspects
{
    [Serializable]
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
            string tabs = string.Empty;
            for (int i = 0; i < _TabCount; i++)
            {
                tabs += "\t";
            }
            return tabs;
        }
    }
}