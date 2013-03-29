using System;
using System.IO;
using AspectMVVM;
using Rhyous.ServiceManager.Aspects;
using Rhyous.ServiceManager.Model;
using Rhyous.ServiceManager.Business;
using System.Reflection;
using System.Xml.Serialization;

namespace Rhyous.ServiceManager.Singletons
{
    [NotifyPropertyChangedClass]
    public class ServiceStore : IPersist
    {
        public const string DEFAULT_FILE = "Services.xml";
        const int REFRESH_TIME = 2000;

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

        [NotifyPropertyChanged]
        public ServiceCollection Services { get; set; }

        [NotifyPropertyChanged]
        [XmlAttribute]
        public string Name { get; set; }

        public void CreateSampleData()
        {
            Services = new ExampleData().Services;
        }

        public void StartContinualRefresh()
        {
            var refresher = new ServiceRefresher();
            refresher.EnableRefreshing(Services, REFRESH_TIME);
        }

        #region IPersist Members
        public static void CreateInstanceFromXml(string inXmlFile)
        {
            if (Load(inXmlFile)
             || Load(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), inXmlFile)))
                Instance.StartContinualRefresh();
        }

        [BackgroundWorkerAspect]
        public void Save()
        {
            Serializer.SerializeToXML(Instance, DEFAULT_FILE);
        }

        public void Load()
        {
            throw new System.NotImplementedException();
        }

        private static bool Load(String path)
        {
            if (File.Exists(path))
            {
                ServiceStore temp = Serializer.DeserializeFromXML<ServiceStore>(path);
                if (_Instance == null)
                { _Instance = temp; }
                else
                {
                    _Instance.Services = temp.Services;
                    _Instance.Name = temp.Name; 
                }
                return true;
            }
            return false;
        }

        public bool IsLoaded
        {
            get { return Instance != null; }
        }

        #endregion
    }
}
