namespace XStreaming.ViewModels
{
    using LibVLCSharp.Shared;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using Xamarin.Forms;

    public class MediaPlayerViewModel
    {
        public Command LoadMediaPlayerCommand { get; set; }

        private LibVLC LibVLC { get; set; }

        private MediaPlayer _mediaPlayer;
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            private set => Set(nameof(MediaPlayer), ref _mediaPlayer, value);
        }
        private void Set<T>(string propertyName, ref T field, T value)
        {
            if (field == null && value != null || field != null && !field.Equals(value))
            {
                field = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private bool IsLoaded { get; set; }
        private bool IsVideoViewInitialized { get; set; }

        public MediaPlayerViewModel()
        {
            //LoadMediaPlayerCommand = 
        }

        private void Play()
        {
            if (IsLoaded && IsVideoViewInitialized)
            {
                MediaPlayer.Play();
            }
        }


    }
}
