

namespace XStreaming
{
    using System;
    using System.ComponentModel;
    using Xamarin.Forms;
    using XStreaming.ViewModels;
    using XStreaming.Models;
    using XStreaming.Views;
    using XStreaming.Services;

    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        RadioStationViewModel viewModel;

        public MainPage(RadiosRepository radioRepository)
        {
            InitializeComponent();
            BindingContext = viewModel = new RadioStationViewModel(radioRepository);
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var station = args.SelectedItem as RadioStation;
            if (station == null)
                return;

            // Manually deselect item.
            ItemsListView.SelectedItem = null;

            viewModel.LoadMediaPlayer(station);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.RadioStations.Count == 0)
                viewModel.LoadRadioStationsCommand.Execute(null);
        }
        void OnPlayButtonClicked(object sender, EventArgs args)
        {
            if (viewModel.IsMediaLoaded)
            {
                viewModel.PlayMediaPlayerCommand.Execute(null);
            }
            else
            {
                // NOTIFY user media not loaded
                DisplayAlert("Select Media", "Pick one station from the list.", "Oki");
            }
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewStationPage()));
        }
    }
}
