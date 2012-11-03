using System;
using System.ServiceProcess;
using System.Xml.Serialization;
using Rhyous.MVVM;

namespace Rhyous.ServiceManager.Model
{
    [Serializable]
    [NotifyPropertyChanged]
    public class Service
    {
        public Service()
        {
            // This enum doesn't have a 0 value, so the regular default
            // won't work, so we decided to set default state to Stopped.
            Status = ServiceControllerStatus.Stopped;
        }

        #region Serialized Properties
        public String ServiceName { get; set; }
        #endregion

        #region Properties Ignored by Serialization
        [XmlIgnore]
        public String DisplayName { get; set; }

        [XmlIgnore]
        public String Description { get; set; }

        [XmlIgnore]
        public ServiceControllerStatus Status { get; set; }

        [XmlIgnore]
        public bool IsInstalled { get; set; }

        [XmlIgnore]
        public StartupType StartupType { get; set; }

        #endregion
    }
}
