namespace XStreaming.ViewModels
{
    using LibVLCSharp.Shared;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XStreaming.Models;
    using XStreaming.Services;
    using XStreaming.Views;

    public class RadioStationViewModel : BaseViewModel, INotifyPropertyChanged
    {

        private LibVLC _LibVLC;
        private MediaPlayer _mediaPlayer;
        private Media _media;
        private bool _isPlaying = false;
        private readonly IDataStore<RadioStation> _radiosRepository;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<RadioStation> RadioStations { get; set; }
        public Command PlayMediaPlayerCommand { get; set; }
        public Command LoadRadioStationsCommand { get; set; }
        public string MediaButtonText { get; set; } = "Play";
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (value == true)
                {
                    MediaButtonText = "Stop";
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MediaButtonText"));
                    _isPlaying = true;
                }
                else
                {
                    MediaButtonText = "Play";
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MediaButtonText"));
                    _isPlaying = false;
                }
            }
        }

        public bool IsMediaLoaded { get; set; } = false;

        public RadioStationViewModel(IDataStore<RadioStation> dataStore)
        {
            _radiosRepository = dataStore;
            Core.Initialize();
            RadioStations = new ObservableCollection<RadioStation>();
            LoadRadioStationsCommand = new Command(async () => await ExecuteLoadRadioStationCommand());
            PlayMediaPlayerCommand = new Command(() => ExecutePlayMediaPlayerCommand());

            MessagingCenter.Subscribe<NewStationPage, RadioStation>(this, "AddItem", async (obj, radioStation) =>
            {
                var newStation = radioStation as RadioStation;
                RadioStations.Add(newStation);
                await _radiosRepository.AddRadioStationAsync(newStation);
            });

            Task.Run(() => SetupVLCObjects());
        }

        public void LoadMediaPlayer(RadioStation radioStation)
        {
            try
            {
                if (_mediaPlayer.IsPlaying)
                {
                    _mediaPlayer.Stop();
                    IsPlaying = false;
                }
                _media = new Media(_LibVLC, radioStation.Url, FromType.FromLocation);
                _mediaPlayer.Media = _media;
                IsMediaLoaded = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                IsMediaLoaded = true;
            }
        }
        //async Task ExecuteLoadMediaPlayerCommand(RadioStation radioStation)
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        RadioStations.Clear();
        //        var radioStations = await DataStore.GetRadioStationsAsync(true);
        //        foreach (var station in radioStations)
        //        {
        //            RadioStations.Add(station);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
        void ExecutePlayMediaPlayerCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (_mediaPlayer.IsPlaying)
                {
                    _mediaPlayer.Stop();
                    IsPlaying = false;
                }
                else
                {
                    _mediaPlayer.Play();
                    IsPlaying = true;
                }
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
        async Task ExecuteLoadRadioStationCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                RadioStations.Clear();
                var radioStations = await _radiosRepository.GetRadioStationsAsync(true);
                foreach (var station in radioStations)
                {
                    RadioStations.Add(station);
                }
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

        void SetupVLCObjects()
        {
            if (_LibVLC == null)
            {
                _LibVLC = new LibVLC();
            }
            if (_mediaPlayer == null)
            {
                _mediaPlayer = new MediaPlayer(_LibVLC) { EnableHardwareDecoding = true };
                _mediaPlayer.Media.AddOption(":no-video");
                _mediaPlayer.SetRole(MediaPlayerRole.Music);
            }
        }
    }
}