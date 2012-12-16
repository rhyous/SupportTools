using System;
using System.IO;
using AspectMVVM;
using Rhyous.ServiceManager.Aspects;
using Rhyous.ServiceManager.Model;
using Rhyous.ServiceManager.Business;

namespace Rhyous.ServiceManager.Singletons
{
    [NotifyPropertyChanged]
    public class ServiceStore : IPersist
    {
        internal ServiceStore()
        {
            //#if DEBUG
            //if (Services == null)
            //    CreateSampleData();
            //Save();
            //#endif
        }

        // TODO: Figure out how to lazy load the Instance of a singleton with PostSharp,
        // because it fails due to the private constructor
        public static ServiceStore Instance
        {
            get { return _Instance ?? (_Instance = new ServiceStore()); }
        } private static ServiceStore _Instance;

        public ServiceCollection Services { get; set; }

        public void CreateSampleData()
        {
            Services = new ExampleData().Services;
        }

        public void StartContinualRefresh()
        {
            var refresher = new ServiceRefresher();
            refresher.EnableRefreshing(Services, 1000);
        }

        #region IPersist Members
        public static void CreateInstanceFromXml()
        {
            if (File.Exists("Services.xml"))
            {
                _Instance = Serializer.DeserializeFromXML<ServiceStore>("Services.xml");
                _Instance.StartContinualRefresh();
            }
        }

        [BackgroundWorkerAspect]
        public void Save()
        {
            Serializer.SerializeToXML(_Instance, "Services.xml");
        }

        public void Load()
        {
            throw new System.NotImplementedException();
        }

        public bool IsLoaded
        {
            get { return _Instance != null; }
        }

        #endregion
    }
}
