using EDDMobileImpl.ViewModels;
using EDMobileLibrary.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace EDDMobileImpl.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HistoryPage : ContentPage
    {
        JournalEntryViewModel viewModel;

        public HistoryPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new JournalEntryViewModel(); 
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    //var item = args.SelectedItem as Item;
        //    //if (item == null)
        //    //    return;

        //    await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

        //    // Manually deselect item.
        //    ItemsListView.SelectedItem = null;
        //}

        //async void AddItem_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        //}



        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.StartListening();

            //if (viewModel?.Items.Count == 0)
            //    viewModel?.LoadItemsCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.StopListening();
        }

        private void Refresh_Clicked(object sender, System.EventArgs e)
        {
            viewModel?.LoadItemsCommand.Execute(null);
        }

    }
}