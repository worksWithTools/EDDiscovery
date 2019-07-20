﻿using EDDMobileImpl;
using EDDMobileImpl.ViewModels;
using EDPlugin;
using EliteDangerous.JSON;
using EliteDangerousCore;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using EDMobileLibrary.Services;

namespace EDMobileLibrary.ViewModels
{
    public class JournalEntryViewModel : BaseViewModel
    {
        ObservableCollection<JournalEntry> items;
        public Command LoadItemsCommand { get; private set; }
        public ObservableCollection<JournalEntry> Items { get => items;  private set => SetProperty(ref items, value); }

        public JournalEntryViewModel() : base()
        {
            Title = "Log";
            Items = new ObservableCollection<JournalEntry>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadHistoryCommand());
            UserDataCache.OnCacheUpdated += UserDataCache_OnCacheUpdated;
        }

        private void UserDataCache_OnCacheUpdated(JournalEntry[] entries)
        {
           
            try
            {
                foreach (var e in entries)
                {
                    Items.Insert(0, e);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }

        private async Task ExecuteLoadHistoryCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                //TODO: limits / ranges 
                var newItems = await JournalEntry.GetAllAsync(commander: EDCommander.CurrentCmdrID, order: "DESC", limit: 100);

                await loadItems(newItems);

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

        private async Task loadItems(System.Collections.Generic.List<JournalEntry> newItems)
        {
            do
            {
                await Task.Run(() =>
                {
                    var chunkSize = Math.Min(10, newItems.Count);
                    var nextChunk = newItems.Take(chunkSize);
                    foreach (var i in nextChunk)
                        Items.Add(i);
                    newItems.RemoveRange(0, chunkSize);
                });
            } while (newItems.Count > 0);
        }
    }
}
