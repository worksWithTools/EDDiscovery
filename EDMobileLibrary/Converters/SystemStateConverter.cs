﻿using EliteDangerousCore;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace EDMobileLibrary.Converters
{
    public class SystemStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((EDState)value).ToString().Replace('_', ' ');
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
