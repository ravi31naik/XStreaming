namespace XStreaming
{
    using Xamarin.Forms;
    using XStreaming.Services;
    using XStreaming.Models;
    using XStreaming.ViewModels;

    public partial class App : Application
    {
        public App(RadiosRepository radioRepository)
        {
            InitializeComponent();
            //DependencyService.Register<IDataStore<RadioStation>, RadiosRepository>();
            MainPage = new NavigationPage(new MainPage(radioRepository));
            //var Page = new MainPage()
            //{
            //    BindingContext = new RadioStationViewModel(radioRepository)
            //};

            //MainPage = new NavigationPage(Page);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
