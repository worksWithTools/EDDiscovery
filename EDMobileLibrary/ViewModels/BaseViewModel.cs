using EDDMobile.Comms;
using EDMobileLibrary.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EDDMobileImpl.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IPropertyChangedNotifier
    {

        public BaseViewModel()
        {
        }

        protected virtual void WebSocket_OnMessage()
        {
            // do nothing
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Debug.WriteLine($"DEBUG: Property {propertyName} changed.");
        }

        public void StartListening()
        {
            // TODO: config - is this really where we want to do this?
            Debug.WriteLine($"TRACE: [{this.GetType().Name}] Starting to listen for websocket messages...");
            App.WebSocket.OnMessage += WebSocket_OnMessage;

        }
        public void StopListening()
        {

            App.WebSocket.OnMessage -= WebSocket_OnMessage;
            Debug.WriteLine($"TRACE: [{this.GetType().Name}] Stopping listening for websocket messages...");
        }

        #endregion
    }
}
