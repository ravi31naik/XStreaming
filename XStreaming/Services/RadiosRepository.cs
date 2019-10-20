

namespace XStreaming.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using XStreaming.Models;

    public class RadiosRepository : IDataStore<RadioStation>
    {
        private readonly DatabaseContext _databaseContext;

        public RadiosRepository(string dbPath)
        {
            _databaseContext = new DatabaseContext(dbPath);
        }

        public async Task<bool> AddRadioStationAsync(RadioStation radioStation)
        {
            try
            {
                var tracking = await _databaseContext.RadioStations.AddAsync(radioStation);
                await _databaseContext.SaveChangesAsync();
                return tracking.State == EntityState.Added;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excetion {ex}");
                return false;
            }
        }

        public Task<bool> DeleteRadioStationAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RadioStation> GetRadioStationAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RadioStation>> GetRadioStationsAsync(bool forceRefresh = false)
        {
            try
            {
                return await _databaseContext.RadioStations.ToListAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Excetion {ex}");
                return null;
            }
        }

        public Task<bool> UpdateRadioStationAsync(RadioStation item)
        {
            throw new NotImplementedException();
        }
    }
}
