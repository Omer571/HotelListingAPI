using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        // seeding data into database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country
                {
                    CountryId = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    CountryId = 2,
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country
                {
                    CountryId = 3,
                    Name = "Cayman Island",
                    ShortName = "CI"
                }
            );

            builder.Entity<Hotel>().HasData(
                new Hotel
                {
                    CountryId = 1,
                    Name = "Sandals Resort and Spa",
                    Address = "Negril",
                    Rating = 4.5,
                    Id = 1,
                },
                new Hotel
                {
                    CountryId = 2,
                    Name = "Grand Palldium",
                    Address = "Nassua",
                    Rating = 4.5,
                    Id = 2,
                },
                new Hotel
                {
                    CountryId = 3,
                    Name = "Marriate",
                    Address = "Gearge Town",
                    Rating = 4,
                    Id = 3,
                }
            );
        }


    }
}
