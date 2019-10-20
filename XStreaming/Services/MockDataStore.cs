namespace XStreaming.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    public class MockDataStore : IDataStore<RadioStation>
    {
        List<RadioStation> RadioStations;

        public MockDataStore()
        {
            RadioStations = new List<RadioStation>();
            var mockRadioStations = new List<RadioStation>
            {
                new RadioStation { Id = Guid.NewGuid().ToString(), Name = "Meethi Mirchi", Description="This is an RadioStation description.",
                Url = "https://meethimirchihdl-lh.akamaihd.net/i/MeethiMirchiHDLive_1_1@320572/master.m3u8" },
                new RadioStation { Id = Guid.NewGuid().ToString(), Name = "Club Mirchi", Description="This is an RadioStation description.",
                Url = "https://clubmirchihdlive-lh.akamaihd.net/i/ClubMirchiHDLive_1_1@336269/master.m3u8" }
            };

            foreach (var RadioStation in mockRadioStations)
            {
                RadioStations.Add(RadioStation);
            }
        }

        public async Task<bool> AddRadioStationAsync(RadioStation RadioStation)
        {
            RadioStations.Add(RadioStation);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateRadioStationAsync(RadioStation RadioStation)
        {
            var oldRadioStation = RadioStations.Where((RadioStation arg) => arg.Id == RadioStation.Id).FirstOrDefault();
            RadioStations.Remove(oldRadioStation);
            RadioStations.Add(RadioStation);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRadioStationAsync(string id)
        {
            var oldRadioStation = RadioStations.Where((RadioStation arg) => arg.Id == id).FirstOrDefault();
            RadioStations.Remove(oldRadioStation);

            return await Task.FromResult(true);
        }

        public async Task<RadioStation> GetRadioStationAsync(string id)
        {
            return await Task.FromResult(RadioStations.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<RadioStation>> GetRadioStationsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(RadioStations);
        }
    }
}
