﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RobotsWorld.Data;
using RobotsWorld.Models;
using RobotsWorld.Services;
using RobotsWorld.Services.Contracts;
using RobotsWorld.Services.Mapper;
using RobotsWorld.Web.HelperMethods;
using RobotsWorld.Web.Hubs;

namespace RobotsWorld.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<RobotsWorldContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<RobotsWorldContext>();
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

            services.AddMvc(options =>
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRobotService, RobotService>();
            services.AddTransient<IAssemblyService, AssemblyService>();
            services.AddTransient<ISubAssemblyService, SubAssemblyService>();
            services.AddTransient<IPartService, PartService>();
            services.AddTransient<IVendorService, VendorService>();
            services.AddTransient<IDeliveryService, DeliveryService>();
            services.AddTransient<ITransportTypeService, TransportTypeService>();
            services.AddTransient<IAdminService, AdminService>();
            
            services.AddAutoMapper(x => x.AddProfile<MapperProfile>());

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Users/Login";
                options.LogoutPath = $"/Users/Logout";
            });

            services.AddSignalR();

            services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.FromSeconds(10));
            services.AddAuthentication()
                .Services.ConfigureApplicationCookie(options =>
                {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSeedRolesMiddleware();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });
        }
    }
}
