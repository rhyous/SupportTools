using System.ServiceProcess;
using Microsoft.Win32;
using Rhyous.ServiceManager.Model;

namespace Rhyous.ServiceManager.Business
{
    public static class ServiceUpdater
    {
        public static void UpdateService(this Service inService)
        {
            var sc = ServiceManager.FindService(inService.ServiceName);
            inService.IsInstalled = (sc != null);
            if (inService.IsInstalled)
            {
                inService.DisplayName = sc.DisplayName;
                inService.Status = sc.Status;
                inService.UpdateDescription();
                inService.UpdateStartupType();
            }
        }

        public static void UpdateState(this Service inService)
        {
            ServiceController sc = ServiceManager.FindService(inService.ServiceName);
            if (sc == null) return;
            inService.Status = sc.Status;
        }

        public static void UpdateDescription(this Service inService)
        {
            RegistryKey key = Registry.LocalMachine;
            key = key.OpenSubKey(@"SYSTEM\CurrentControlSet\services\" + inService.ServiceName);
            if (key != null)
            {
                inService.Description = key.GetValue("Description") as string;
            }
        }

        public static void UpdateStartupType(this Service inService)
        {
            RegistryKey key = Registry.LocalMachine;
            key = key.OpenSubKey(@"SYSTEM\CurrentControlSet\services\" + inService.ServiceName);
            if (key != null)
            {
                StartupType type = (StartupType)key.GetValue("Start");
                inService.StartupType = type;
            }
        }
    }
}
