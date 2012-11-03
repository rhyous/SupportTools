using System;
using System.ComponentModel;
using PostSharp.Aspects;

namespace Rhyous.ServiceManager.Aspects
{
    [Serializable]
    [BackgroundWorkerAspect(AttributeExclude = true)]
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
