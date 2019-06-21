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
        StatusViewModel _LastJournalEntry;
        
        public StatusPage()
        {
            InitializeComponent();

            Status = new StatusViewModel();
            BindingContext = this;
        }

        public StatusViewModel Status { get => _LastJournalEntry; set => _LastJournalEntry = value; }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Status.LoadItemsCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Status.StartListening();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Status.StopListening();
        }
    }
}