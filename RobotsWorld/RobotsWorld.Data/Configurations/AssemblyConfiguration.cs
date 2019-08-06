using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RobotsWorld.Models;

namespace RobotsWorld.Data.Configurations
{
    public class AssemblyConfiguration : IEntityTypeConfiguration<Assembly>
    {
        public void Configure(EntityTypeBuilder<Assembly> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            //builder.Property(x => x.TotalPrice).IsRequired();

            builder.HasMany(x => x.Robots)
                .WithOne(x => x.Assembly)
                .HasForeignKey(x => x.AssemblyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SubAssemblies)
                .WithOne(x => x.Assembly)
                .HasForeignKey(x => x.AssemblyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
