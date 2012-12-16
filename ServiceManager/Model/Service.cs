using System;
using System.ServiceProcess;
using System.Xml.Serialization;
using AspectMVVM;

namespace Rhyous.ServiceManager.Model
{
    [Serializable]
    [NotifyPropertyChangedClass]
    public class Service
    {
        public Service()
        {
            // This enum doesn't have a 0 value, so the regular default
            // won't work, so we decided to set default state to Stopped.
            Status = ServiceControllerStatus.Stopped;
        }

        #region Serialized Properties
        [NotifyPropertyChanged]
        public String ServiceName { get; set; }
        #endregion

        #region Properties Ignored by Serialization
        [NotifyPropertyChanged]
        [XmlIgnore]
        public String DisplayName { get; set; }

        [XmlIgnore]
        [NotifyPropertyChanged]
        public String Description { get; set; }

        [XmlIgnore]
        [NotifyPropertyChanged]
        public ServiceControllerStatus Status { get; set; }

        [XmlIgnore]
        [NotifyPropertyChanged]
        public bool IsInstalled { get; set; }

        [XmlIgnore]
        [NotifyPropertyChanged]
        public StartupType StartupType { get; set; }

        #endregion
    }
}
