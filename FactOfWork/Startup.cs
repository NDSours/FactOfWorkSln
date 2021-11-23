using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FactOfWork.Models;
using FactOfWork.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace FactOfWork
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IConfiguration config) => Configuration = config;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<SqlDbContext>(opts =>
                opts.UseMySql(Configuration.GetConnectionString("MySqlConnection"),new MySqlServerVersion(new Version(8,0,27))));
            services.AddScoped<IRepositoryPerson, RepositoryPerson>();
            services.AddScoped<IRepositoryFile, RepositoryFile>();
            services.AddServerSideBlazor();

            services.AddDbContext<IdentityContext>(opts =>
                opts.UseMySql(Configuration.GetConnectionString("IdentityMySqlConnection"),new MySqlServerVersion(new Version(8,0,27))));
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
            services.Configure<IdentityOptions>(opts =>
            {
                opts.Password.RequiredLength = 6; // минимальная длина пароля
                opts.Password.RequireNonAlphanumeric = false; // можно без цифр
                opts.Password.RequireLowercase = false; // не важен регистр букв
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false; //не обязательны спец символы
            });

            //проверка достоверности модели
            services.Configure<MvcOptions>(opts =>
                opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value => "Пожалуйста введите данные"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
            });
            SeedData.EnsurePopulated(app);
            IdentitySeedData.CreateAdminAccount(app.ApplicationServices, Configuration);
        }
    }
}
