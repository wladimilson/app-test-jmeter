using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_test_jmeter.Data;
using app_test_jmeter.Models;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace app_test_jmeter
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
            services.AddMvc();

            services.AddDbContext<AdmContext>(options => 
                //Create hiddenSettings.json with the connection string
                options.UseNpgsql(Configuration.GetValue<string>("Database:ConnectionString"))
            );

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AdmContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/accessdenied";
                    options.ExpireTimeSpan = new TimeSpan(0, 15, 0);
                    options.ReturnUrlParameter = "returnurl";
                });
            
            services.AddSingleton <IConfiguration> (Configuration);
            services.AddTransient<ISeedData, SeedData>();

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "login-route",
                    template: "login",
                    defaults: new { controller = "Account", action = "Login" }
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.Run(app.ApplicationServices).Wait();
        }
    }
}
