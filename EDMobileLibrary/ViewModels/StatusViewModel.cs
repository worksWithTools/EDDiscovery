﻿using EDPlugin;
using EliteDangerousCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EDDMobileImpl.ViewModels
{
    public class StatusViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; private set; }
        public StatusViewModel()
        {
            Title = "Status";
            LoadItemsCommand = new Command(async () => await ExecuteLoadJournalEntriesCommand());
        }


        protected override void WebSocket_OnMessage()
        {
            App.WebSocket.TryGetMessage(out string msg);
            RefreshStatus(msg);
        }

       
        async Task ExecuteLoadJournalEntriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await App.WebSocket.Send(WebSocketMessage.REFRESH_STATUS);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void RefreshStatus(string msg)
        {
            //TODO: improve the deserialization.
            var lastEntry = HistoryEntry.FromJSON(msg);
            
            if (lastEntry != null)
            {
                this.lastEntry = lastEntry;
                OnPropertyChanged(null); // should result in all props being refreshed.
            }

        }
        //TODO: why does this crash (only on first message... which perhaps isn't a complete system?)
        public String WhereAmI {
            get => lastEntry?.WhereAmI ?? "Unknown";
        }
        public String Note
        {
            get => lastEntry?.snc?.Note ?? "";
        }
        public ISystem System { get => lastEntry?.System; }

        private HistoryEntry lastEntry;
    }
}