using System;
using System.IO;
using AspectMVVM;
using Rhyous.ServiceManager.Aspects;
using Rhyous.ServiceManager.Business;

namespace Rhyous.ServiceManager.Singletons
{
    public enum ServiceColumn
    {
        Status,
        DisplayName,
        ServiceName,
        Description,
        IsInstalled,
        StartupType,
        StartStopButton
    }

    [NotifyPropertyChangedClass(AspectPriority = 10)]
    [PersistOnSetAspect(AspectPriority = 20)]
    public class ColumnSettings : IPersist
    {
        internal ColumnSettings()
        {
            Load();
        }

        [PersistOnSetAspect(AttributeExclude = true)]
        public static ColumnSettings Instance { get; private set; }

        public bool IsLoaded
        {
            get { return Instance != null; }
        }

        [NotifyPropertyChanged]
        public bool ShowDisplayNameColumn { get; set; }

        [NotifyPropertyChanged]
        public bool ShowServiceNameColumn { get; set; }

        [NotifyPropertyChanged]
        public bool ShowDescriptionColumn { get; set; }

        [NotifyPropertyChanged]
        public bool ShowStatusColumn { get; set; }

        [NotifyPropertyChanged]
        public bool ShowIsInstalledColumn { get; set; }

        [NotifyPropertyChanged]
        public bool ShowStartupTypeColumn { get; set; }

        [NotifyPropertyChanged]
        [DefaultValueAspect(DefaultValue = ServiceColumn.Status, EmptyValue = 0)]
        public int StatusColumnIndex { get; set; }

        [NotifyPropertyChanged]
        [DefaultValueAspect(DefaultValue = ServiceColumn.DisplayName, EmptyValue = 0)]
        public int DisplayNameColumnIndex { get; set; }

        [NotifyPropertyChanged]
        [DefaultValueAspect(DefaultValue = ServiceColumn.ServiceName, EmptyValue = 0)]
        public int ServiceNameColumnIndex { get; set; }

        [NotifyPropertyChanged]
        [DefaultValueAspect(DefaultValue = ServiceColumn.Description, EmptyValue = 0)]
        public int DescriptionColumnIndex { get; set; }

        [NotifyPropertyChanged]
        [DefaultValueAspect(DefaultValue = ServiceColumn.IsInstalled, EmptyValue = 0)]
        public int IsInstalledColumnIndex { get; set; }

        [NotifyPropertyChanged]
        [DefaultValueAspect(DefaultValue = ServiceColumn.StartupType, EmptyValue = 0)]
        public int StartupTypeColumnIndex { get; set; }

        [NotifyPropertyChanged]
        [DefaultValueAspect(DefaultValue = ServiceColumn.StartStopButton, EmptyValue = 0)]
        public int StartStopButtonColumnIndex { get; set; }

        public void Load()
        {
            ShowDescriptionColumn = ShowDisplayNameColumn = ShowServiceNameColumn = ShowIsInstalledColumn = ShowStartupTypeColumn = ShowStatusColumn = true;
        }

        [BackgroundWorkerAspect]
        public void Save()
        {
            String fullpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Service Manager", "Settings", "ColumnSettings.xml");
            Serializer.SerializeToXML(Instance, fullpath);
        }

        public static void CreateInstanceFromXml()
        {
            String fullpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Service Manager", "Settings", "ColumnSettings.xml");
            if (File.Exists(fullpath))
            {
                Instance = Serializer.DeserializeFromXML<ColumnSettings>(fullpath);
            }
            else
            {
                Instance = new ColumnSettings();
            }
        }
    }
}
