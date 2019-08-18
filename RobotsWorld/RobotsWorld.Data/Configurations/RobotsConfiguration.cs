using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RobotsWorld.Models;

namespace RobotsWorld.Data.Configurations
{
    public class RobotsConfiguration : IEntityTypeConfiguration<Robot>
    {
        public void Configure(EntityTypeBuilder<Robot> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.SerialNumber).IsRequired();

            builder.Property(x => x.Axes).IsRequired();

            builder.Property(x => x.ImageUrl).IsRequired(false);

            builder.HasOne(x => x.Assembly)
                .WithOne(x => x.Robot)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Robots)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
