using System;

using Xamarin.Forms;
using XStreaming.Models;

namespace XStreaming.Views
{
    public partial class NewStationPage : ContentPage
    {
        public RadioStation radioStation { get; set; }
        public NewStationPage()
        {
            InitializeComponent();

            radioStation = new RadioStation
            {
                Description = "New Radio Station Description",
                Name = "Radio Station Name",
                Url = "Radio Station Link"
            };
            BindingContext = this;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {

            MessagingCenter.Send(this, "AddItem", radioStation);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}