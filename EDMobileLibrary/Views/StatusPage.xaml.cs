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
        JournalEntryViewModel viewModel;

        public StatusPage()
        {
            InitializeComponent();


            BindingContext = viewModel = new JournalEntryViewModel();
        }

  

        private void Button_Clicked(object sender, EventArgs e)
        {
            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}