using System;
using System.Windows.Data;
using Rhyous.ServiceManager.Model;

namespace Rhyous.ServiceManager.Converters
{
    class MultipleBoolsConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool b1 = (bool)values[0];
            bool b2 = (StartupType)values[1] != StartupType.Disabled;
            return b1 && b2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
