using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prio.BL.Database;
using Prio.BL.Database.ModelLogic;
using Prio.BL.MainLogic;
using Prio.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prio.MVC
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
            var username = "erik";
            var password = "Logon123";

            var context = new PrioContext(username, password);
            var toDoLogic = new TodoLogic(context);
            var aktivitetLogic = new AktivitetLogic(context, toDoLogic);
            var mainLogic = new DatLogic(aktivitetLogic, toDoLogic);

            services.Add(new ServiceDescriptor(typeof(ITodoLogic), toDoLogic));
            services.Add(new ServiceDescriptor(typeof(IAktivitetLogic), aktivitetLogic));
            services.Add(new ServiceDescriptor(typeof(IDatLogic), mainLogic));

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication", config =>
            {
                config.Cookie.Name = "UserLoginCookie";
                config.LoginPath = "/Account/Login";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
