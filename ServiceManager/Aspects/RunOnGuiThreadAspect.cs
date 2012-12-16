using System;
using System.Windows;
using System.Windows.Threading;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

namespace Rhyous.ServiceManager.Aspects
{
    [Serializable]
    [RunOnGuiThreadAspect(AttributeExclude = true)]
    [ProvideAspectRole(StandardRoles.Threading)]
    public class RunOnGuiThreadAspect : MethodInterceptionAspect
    {
        public override void OnInvoke(
            MethodInterceptionArgs eventArgs)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                // We are already in the GUI thread. Proceed.
                eventArgs.Proceed();
            }
            else
            {
                // Invoke the target method synchronously.  
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(eventArgs.Proceed));
            }
        }
    }
}
