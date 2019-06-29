using System;
using System.Collections.Generic;
using System.Text;

namespace EDMobileLibrary.Services
{
    public interface IPropertyChangedNotifier
    {
        void OnPropertyChanged(string propName);
    }
}
