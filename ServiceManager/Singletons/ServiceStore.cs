using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using AspectMVVM;
using Rhyous.ServiceManager.Aspects;
using Rhyous.ServiceManager.Business;
using Rhyous.ServiceManager.Model;

namespace Rhyous.ServiceManager.Singletons
{
    [NotifyPropertyChangedClass]
    public class ServiceStore : IPersist
    {
        public const string DefaultFile = "Services.xml";
        const int RefreshTime = 2000;

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
            refresher.EnableRefreshing(Services, RefreshTime);
        }

        #region IPersist Members
        public static void CreateInstanceFromXml(string inXmlFile)
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            if (Load(inXmlFile) || Load(Path.Combine(dir, inXmlFile)))
                Instance.StartContinualRefresh();
        }

        [BackgroundWorkerAspect]
        public void Save()
        {
            Serializer.SerializeToXml(Instance, DefaultFile);
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        private static bool Load(string path)
        {
            if (File.Exists(path))
            {
                var temp = Serializer.DeserializeFromXml<ServiceStore>(path);
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
