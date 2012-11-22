using System;
using System.IO;
using Rhyous.MVVM;
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

    [NotifyPropertyChanged(AspectPriority = 10)]
    [PersistOnSetAspect(AspectPriority = 20)]
    public class ColumnSettings : IPersist
    {
        private static bool IsInstanceLoaded;


        static ColumnSettings()
        {

        }
        public ColumnSettings()
        {
        }

        // TODO: Figure out how to lazy load the Instance of a singleton,
        // because it fails due to the private constructor

        [PersistOnSetAspect(AttributeExclude = true)]
        public static ColumnSettings Instance { get; private set; }

        public bool IsLoaded
        {
            get { return Instance != null; }
        }

        public bool ShowDisplayNameColumn { get; set; }

        public bool ShowServiceNameColumn { get; set; }

        public bool ShowDescriptionColumn { get; set; }

        public bool ShowStatusColumn { get; set; }

        public bool ShowIsInstalledColumn { get; set; }

        public bool ShowStartupTypeColumn { get; set; }

        [DefaultValueAspect(DefaultValue = ServiceColumn.Status, EmptyValue = 0)]
        public int StatusColumnIndex { get; set; }

        [DefaultValueAspect(DefaultValue = ServiceColumn.DisplayName, EmptyValue = 0)]
        public int DisplayNameColumnIndex { get; set; }

        [DefaultValueAspect(DefaultValue = ServiceColumn.ServiceName, EmptyValue = 0)]
        public int ServiceNameColumnIndex { get; set; }

        [DefaultValueAspect(DefaultValue = ServiceColumn.Description, EmptyValue = 0)]
        public int DescriptionColumnIndex { get; set; }

        [DefaultValueAspect(DefaultValue = ServiceColumn.IsInstalled, EmptyValue = 0)]
        public int IsInstalledColumnIndex { get; set; }

        [DefaultValueAspect(DefaultValue = ServiceColumn.StartupType, EmptyValue = 0)]
        public int StartupTypeColumnIndex { get; set; }

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
            String fullpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                                           "Service Manager", "Settings", "ColumnSettings.xml");
            if (File.Exists(fullpath))
                Instance = Serializer.DeserializeFromXML<ColumnSettings>(fullpath);
        }
    }
}
