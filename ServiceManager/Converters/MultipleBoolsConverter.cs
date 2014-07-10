using System;
using System.Globalization;
using System.Windows.Data;
using Rhyous.ServiceManager.Model;

namespace Rhyous.ServiceManager.Converters
{
    class MultipleBoolsConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var b1 = (bool)values[0];
            var b2 = (StartupType)values[1] != StartupType.Disabled;
            return b1 && b2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
