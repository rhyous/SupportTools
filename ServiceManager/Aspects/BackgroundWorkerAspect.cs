using System;
using System.ComponentModel;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

namespace Rhyous.ServiceManager.Aspects
{
    [Serializable]
    [BackgroundWorkerAspect(AttributeExclude = true)]
    [ProvideAspectRole(StandardRoles.Threading)]
    public class BackgroundWorkerAspect : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (sender, e) => args.Proceed();
            bw.RunWorkerAsync();
        }
    }
}
