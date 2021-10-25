using HotelListingAPI.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListingAPI.Data
{
    public class DatabaseContext : IdentityDbContext<ApiUser> // identity for security
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }

        // seeding data into database
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // added for security (base class = IdentityDbContext)
            base.OnModelCreating(builder);

            // fill db with this data
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new HotelConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());

        }


    }
}
