using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RobotsWorld.Models;

namespace RobotsWorld.Data.Configurations
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.SentOn).IsRequired();

            builder.Property(x => x.StartingPoint).IsRequired();

            builder.Property(x => x.DestinationPoint).IsRequired();

            builder.HasOne(x => x.Receiver)
                .WithMany(x => x.ReceivedRobots)
                .HasForeignKey(x => x.ReceiverId);

            builder.HasOne(x => x.Sender)
                .WithMany(x => x.SentRobots)
                .HasForeignKey(x => x.SenderId);

            builder.HasOne(x => x.TransportType)
                .WithMany(x => x.Deliveries)
                .HasForeignKey(x => x.TransportTypeId);
        }
    }
}
