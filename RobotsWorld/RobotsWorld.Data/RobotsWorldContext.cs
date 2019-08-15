using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RobotsWorld.Data.Configurations;
using RobotsWorld.Models;

namespace RobotsWorld.Data
{
    public class RobotsWorldContext : IdentityDbContext<User>
    {
        public DbSet<Robot> Robots { get; set; }
        public DbSet<Assembly> Assemblies { get; set; }
        public DbSet<SubAssembly> SubAssemblies { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }


        public RobotsWorldContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AssemblyConfiguration());
            builder.ApplyConfiguration(new SubAssemblyConfiguration());
            builder.ApplyConfiguration(new RobotsConfiguration());
            builder.ApplyConfiguration(new PartConfiguration());
            builder.ApplyConfiguration(new VendorConfiguration());
            builder.ApplyConfiguration(new DeliveryConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
