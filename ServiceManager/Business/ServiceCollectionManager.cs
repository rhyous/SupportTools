using Rhyous.ServiceManager.Model;

namespace Rhyous.ServiceManager.Business
{
    public static class ServiceCollectionManager
    {
        public static void StartServices(this ServiceCollection inServiceCollection)
        {
            foreach (Service service in inServiceCollection)
            {
                service.Start();
            }
        }

        public static void StopServices(this ServiceCollection inServiceCollection)
        {
            foreach (Service service in inServiceCollection)
            {
                service.Stop();
            }
        }
    }
}
