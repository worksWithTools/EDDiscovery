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
    
    public partial class StatusPage : global::Xamarin.Forms.CarouselPage
    {
        StatusViewModel _LastJournalEntry;
        
        public StatusPage()
        {
            InitializeComponent();
            _LastJournalEntry = new StatusViewModel();

            BindingContext = _LastJournalEntry;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _LastJournalEntry.LoadItemsCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _LastJournalEntry.StartListening();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _LastJournalEntry.StopListening();
        }
    }
}