using EDDMobile.Comms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EDDMobileImpl.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public WebSocketWrapper WebSocket { get; private set; }

        private Task _listeningTask;

        public BaseViewModel()
        {
            WebSocket = new WebSocketWrapper();
            Task.Run(async () => await WebSocket.Connect("ws://192.168.0.32/eddmobile"));

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
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartListening()
        {
            // TODO: config - is this really where we want to do this?
            WebSocket.OnMessage += WebSocket_OnMessage;
            _listeningTask = Task.Run(async () => {
                await WebSocket.Listen();
            });
        }
        public async void StopListening()
        {
            WebSocket.OnMessage -= WebSocket_OnMessage;
            await WebSocket.Disconnect();
            _listeningTask = null;
        }

        #endregion
    }
}
