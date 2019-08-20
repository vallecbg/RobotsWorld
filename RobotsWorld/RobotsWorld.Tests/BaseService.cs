using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services;
using RobotsWorld.Services.Contracts;
using RobotsWorld.Services.Mapper;
using RobotsWorld.Web.HelperMethods;

namespace RobotsWorld.Tests
{
    [TestFixture]
    public class BaseService
    {
        protected IServiceProvider Provider { get; set; }

        protected RobotsWorldContext Context { get; set; }

        [SetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(x => { x.AddProfile<MapperProfile>(); });
            var services = SetServices();
            this.Provider = services.BuildServiceProvider();
            this.Context = this.Provider.GetRequiredService<RobotsWorldContext>();
            SetScoppedServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
            this.Context.Database.EnsureDeleted();
        }

        private void SetScoppedServiceProvider()
        {
            var httpContext = this.Provider.GetService<IHttpContextAccessor>();
            httpContext.HttpContext.RequestServices = this.Provider.CreateScope().ServiceProvider;
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RobotsWorldContext>(
                cfg => cfg.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRobotService, RobotService>();
            services.AddScoped<IAssemblyService, AssemblyService>();
            services.AddScoped<ISubAssemblyService, SubAssemblyService>();
            services.AddScoped<IPartService, PartService>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<ITransportTypeService, TransportTypeService>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 0;
            })
                .AddEntityFrameworkStores<RobotsWorldContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper();

            var context = new DefaultHttpContext();

            services.AddSingleton<IHttpContextAccessor>(
                new HttpContextAccessor()
                {
                    HttpContext = context
                });

            return services;
        }
    }
}
