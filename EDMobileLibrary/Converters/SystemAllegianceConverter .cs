using EliteDangerousCore;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace EDMobileLibrary.Converters
{
    public class SystemAllegianceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((EDAllegiance)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
