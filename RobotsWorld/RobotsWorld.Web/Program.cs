﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RobotsWorld.Data;
using RobotsWorld.Models;

namespace RobotsWorld.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var dbContext = serviceProvider.GetRequiredService<RobotsWorldContext>();

                dbContext.Database.EnsureCreated();

                SeedVendorsIfDbEmpty(serviceProvider).GetAwaiter().GetResult();
                //TODO: I can seed some information to the database
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static async Task SeedVendorsIfDbEmpty(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<RobotsWorldContext>();

            //Make sure the database is created
            dbContext.Database.EnsureCreated();

            var vendors = new[]
            {
                new Vendor()
                {
                    Name = "Mitsubishi Electric"
                },
                new Vendor()
                {
                    Name = "IGM"
                },
                new Vendor()
                {
                    Name = "Milara International"
                },
            };

            var areAnyVendors = dbContext.Vendors.Any();
            if (!areAnyVendors)
            {
                dbContext.Vendors.AddRange(vendors);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
