using System;
using System.ServiceProcess;
using System.Windows.Data;

namespace Rhyous.ServiceManager.Converters
{
    public class ServiceStatusToStartStopStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(ServiceControllerStatus.Stopped))
                return "Start";
            if (value.Equals(ServiceControllerStatus.Running))
                return "Stop";
            return "Wait";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
