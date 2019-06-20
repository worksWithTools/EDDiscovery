using EDDMobile.Comms;
using EDDMobileImpl.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EDDMobileImpl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusPage : ContentPage
    {
        JournalEntryViewModel _LastJournalEntry;
        
        public StatusPage()
        {
            InitializeComponent();

            Status = new JournalEntryViewModel();
            BindingContext = this;
        }

        public JournalEntryViewModel Status { get => _LastJournalEntry; set => _LastJournalEntry = value; }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Status.LoadItemsCommand.Execute(null);
        }
    }
}