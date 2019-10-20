namespace XStreaming.Services
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;
    using XStreaming.Models;

    public class DatabaseContext : DbContext
    {
        public DbSet<RadioStation> RadioStations { get; set; }

        private readonly string _databasePath;

        public DatabaseContext(string databasePath)
        {
            _databasePath = databasePath;
            //Task.Run(async ()=> await Database.EnsureCreatedAsync());
            var sure = Database.EnsureCreated();
            Console.WriteLine($"Sure {sure}");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite($"FileName={_databasePath}");
        }
    }
}
