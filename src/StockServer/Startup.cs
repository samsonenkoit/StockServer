using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockServer.Data;
using StockServer.Models;
using StockServer.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using StockServer.DL;
using StockServer.BL.DataProvider.Interface;
using StockServer.DL.DataProvider;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace StockServer
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Cookies.ApplicationCookie.AuthenticationScheme = "T1";
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            ConfigMapper();
            services.AddScoped<IMapper>(_ => Mapper.Instance);
            services.AddScoped<StockDbEntities>(_ => new StockDbEntities(Configuration.GetConnectionString("StockDbEfConnection")));

            services.AddTransient<IPlaceProvider, PlaceProvider>();
            services.AddTransient<IOfferProvider, OfferProvider>();


            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        private void ConfigMapper()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ViewModelToModelMappingProfile>();
                x.AddProfile<EntityToEntityDefaultMappingProfile>();
            });
            Mapper.AssertConfigurationIsValid();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRequestLocalization((new RequestLocalizationOptions()
            {
                
                SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en")
                }
            }));

            app.UseStaticFiles();

            ConfigureAuth(app);

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "apiRoute",
                    template: "api/v1/{controller}/{action}/{id?}",
                    defaults: new { area = "api" });
                
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Place}/{action=Index}/{id?}",
                    defaults: new { area = ""});
            });
        }

    }
}
