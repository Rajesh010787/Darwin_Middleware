using DB_Middleware.DAO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Middleware
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
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
                options.Cookie.HttpOnly = true; // Make the session cookie HTTP-only
                options.Cookie.IsEssential = true; // Make the session cookie essential for the application
            });
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<INotification, NotificationService>();
       
            services.AddControllersWithViews();
          
            //services.AddControllersWithViews(); // or services.AddRazorPages() for Razor Pages
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
           
            services.AddHttpContextAccessor();

            // 🔐 Add Data Protection
         

      //      services.AddDataProtection()
      //.PersistKeysToFileSystem(
      //    new DirectoryInfo(@"C:\DataProtectionKeys"))
      //.SetApplicationName("DB_Middleware");

      //      // 🔐 Register SalaryProtector
      //      services.AddScoped<SalaryProtector>();
        }

    
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession(); // Make sure this is included
            app.UseRouting();

            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Inter}/{action=User_Login}/{id?}");
            });
        }
    }
}
