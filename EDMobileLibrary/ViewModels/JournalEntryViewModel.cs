using EDPlugin;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using JournalEntry = EDPlugin.EDDDLLIF.JournalEntry;

namespace EDDMobileImpl.ViewModels
{
    public class JournalEntryViewModel : BaseViewModel
    {
        JournalEntry lastJournalEntry;
        public Command LoadItemsCommand { get; set; }
        public string WhereAmI { get => lastJournalEntry.whereami; set => SetProperty(ref lastJournalEntry.whereami, value); }

        public JournalEntryViewModel()
        {
            Title = "Browse";
            LoadItemsCommand = new Command(async () => await ExecuteLoadJournalEntriesCommand());
        }

        async Task ExecuteLoadJournalEntriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await WebSocket.Send("refresh");
                var msg = await WebSocket.ListenForMessage();
                
                var lastEntry = JsonConvert.DeserializeObject<JournalEntry>(msg);
                PropertyCopier<JournalEntry, JournalEntryViewModel>.Copy(lastEntry, this);

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
    }

    public class PropertyCopier<TSource, TTarget> where TSource : struct
                                            where TTarget : class
    {
        public static void Copy(TSource source, TTarget target)
        {
            var sourceFields = source.GetType().GetFields();
            var targetProps = target.GetType().GetProperties();

            foreach (var sourceField in sourceFields)
            {
                foreach (var targetProp in targetProps)
                {
                    if (sourceField.Name.EqualsAlphaNumOnlyNoCase(targetProp.Name) && sourceField.FieldType == targetProp.PropertyType)
                    {
                        targetProp.SetValue(target, sourceField.GetValue(source));
                        break;
                    }
                }
            }
        }
    }
}