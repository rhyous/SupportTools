using System;
using System.ServiceProcess;
using Rhyous.ServiceManager.Aspects;
using Rhyous.ServiceManager.Model;
using Rhyous.ServiceManager.Singletons;
using TimeoutException = System.ServiceProcess.TimeoutException;

namespace Rhyous.ServiceManager.Business
{
    public static class ServiceManager
    {
        public static TimeSpan TimeOut = TimeSpan.FromMilliseconds(25000);

        public static Service GetService(string inServiceName)
        {
            var service = new Service { ServiceName = inServiceName };
            service.UpdateService();
            return service;
        }

        [ExceptionAspect(ExceptionType = typeof(InvalidOperationException), Message = "InvalidOperationException starting service.")]
        [ExceptionAspect(ExceptionType = typeof(TimeoutException), Message = "TimeoutException starting service.")]
        public static void Start(this Service inService)
        {
            var sc = FindService(inService.ServiceName);
            var st = sc.ServiceType;
            if (sc == null)
                return;
            inService.UpdateService();
            if (inService.Status != ServiceControllerStatus.Running && inService.StartupType != StartupType.Disabled)
            {
                Log.WriteLine("Attempting to start service: " + sc.DisplayName);
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(10000));
            }
        }

        [ExceptionAspect(ExceptionType = typeof(InvalidOperationException), Message = "Exception stopping service.")]
        [ExceptionAspect(ExceptionType = typeof(TimeoutException), Message = "TimeoutException stopping service.")]
        public static void Stop(this Service inService, bool inShouldRestart = false)
        {
            var sc = FindService(inService.ServiceName);

            if (sc == null)
                return;

            if (sc.Status != ServiceControllerStatus.Stopped)
            {
                Log.WriteLine("Attempting to stop service: " + sc.DisplayName);
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMilliseconds(30000));
            }

            if (inShouldRestart)
                inService.Start();
        }

        public static ServiceController FindService(string inServiceName)
        {
            var services = ServiceController.GetServices();

            foreach (ServiceController service in services)
            {
                if (service.ServiceName == inServiceName)
                    return new ServiceController(inServiceName); ;
            }

            return null;
        }
    }
}
