using System;
using System.Threading;
using Rhyous.ServiceManager.Aspects;
using Rhyous.ServiceManager.Model;

namespace Rhyous.ServiceManager.Business
{
    public class ServiceRefresher
    {
        public bool RefreshEnabled { get; private set; }

        public bool IsRefreshing { get; private set; }

        public void EnableRefreshing(ServiceCollection inServiceCollection, int inMilliseconds)
        {
            if (IsRefreshing)
                return;

            RefreshEnabled = IsRefreshing = true;
            RefreshServices(inServiceCollection, TimeSpan.FromMilliseconds(inMilliseconds));
        }

        public void DisableRefreshing()
        {
            RefreshEnabled = true;
        }

        [BackgroundWorkerAspect]
        private void RefreshServices(ServiceCollection inServices, TimeSpan inTimeSpan)
        {
            while (RefreshEnabled)
            {
                foreach (var service in inServices)
                {
                    service.UpdateService();
                }
                Thread.Sleep(inTimeSpan);
            }
        }
    }
}
