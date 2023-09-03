using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Country country1 = new Country
            {
                CountryId = 1,
                Name = "Pakistan",
                ShortName = "PK"
            };

            Country country2 = new Country 
            {
                CountryId = 2,
                Name = "India",
                ShortName = "IND"
            };

            Country country3 = new Country
            {
                CountryId = 3,
                Name = "United States",
                ShortName = "USA"
            };

            modelBuilder.Entity<Country>().HasData(
                country1,
                country2,
                country3
                
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    HotelId = 1,
                    Name = "Quetta Hotel",
                    Address = "Machi miani road",
                    CountryId = 1, // Set the CountryId directly
                    Rating = 4.5
                },
                new Hotel
                {
                    HotelId = 2,
                    Name = "Hindu Hotel",
                    Address = "Bahawalpur road",
                    CountryId = 2, // Set the CountryId directly
                    Rating = 4.0
                },
                new Hotel
                {
                    HotelId = 3,
                    Name = "Burger King",
                    Address = "Mountpalace burmingham",
                    CountryId = 3, // Set the CountryId directly
                    Rating = 4.2
                }
            );
        }


        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
    }
}

