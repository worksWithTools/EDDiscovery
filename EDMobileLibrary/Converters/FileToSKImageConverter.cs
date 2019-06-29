using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace EDMobileLibrary.Converters
{
    public class FileToSKImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
#if GORILLA
            var jes = EliteDangerousCore.JournalEntry.GetNameImageOfEvent(value.ToString());
            return jes.Item3; // Item3 is the image
#else
            return value;
#endif           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
