using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Rhyous.ServiceManager.Model
{
    [Serializable()]
    [XmlRoot("Services")]
    public class ServiceCollection : ObservableCollection<Service>
    {
    }
}
