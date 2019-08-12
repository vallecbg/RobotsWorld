using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RobotsWorld.Models;

namespace RobotsWorld.Data.Configurations
{
    public class SubAssemblyConfiguration : IEntityTypeConfiguration<SubAssembly>
    {
        public void Configure(EntityTypeBuilder<SubAssembly> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.Weight).IsRequired();

            builder.Property(x => x.ImageUrl).IsRequired(false);

            builder.HasMany(x => x.Parts)
                .WithOne(x => x.SubAssembly)
                .HasForeignKey(x => x.SubAssemblyId);
        }
    }
}
