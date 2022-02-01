using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RazorViewMVCDemo.Data;
using RazorViewMVCDemo.Models;

namespace RazorViewMVCDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _env = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            if(_env.IsDevelopment())
                services.AddDbContextPool<AppDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("ConSqlite")));

            // connection string for sql server
            //services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Con")));

            // connection string for sql sqlite
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConDocker")));

            services.AddIdentity<User, IdentityRole>(opt =>
            {
                //opt.Password.RequireUppercase = false;
                //opt.Password.RequireUppercase = false;
                //opt.Password.RequiredLength = 1;
                //opt.Password.RequiredUniqueChars = 1;
                //opt.Password.RequireNonAlphanumeric = false;

                // opt.SignIn.RequireConfirmedAccount = true;

            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<Seeder>();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //var envName = env.WebRootPath;

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            seeder.SeedMe().Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
