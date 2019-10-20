namespace XStreaming.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDataStore<RadioStation>
    {
        Task<bool> AddRadioStationAsync(RadioStation item);
        Task<bool> UpdateRadioStationAsync(RadioStation item);
        Task<bool> DeleteRadioStationAsync(string id);
        Task<RadioStation> GetRadioStationAsync(string id);
        Task<IEnumerable<RadioStation>> GetRadioStationsAsync(bool forceRefresh = false);
    }
}
