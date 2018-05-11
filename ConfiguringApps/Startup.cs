using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfiguringApps
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc().AddMvcOptions(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });
            #region Depricated - 5/10/2018 Introduced AddMvcOptions
            /*
            services.AddMvc();
            */
            #endregion

        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{Id?}");
            });
        }

        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseBrowserLink();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        #region Depricated - 5/11/2018 Implemented Separate Dev and Production Configure Methods
        /*
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if((Configuration.GetSection("ShortCircuitMiddleware")?.GetValue<bool>("EnableBrowserShortCircuit")).Value)
    {
        app.UseMiddleware<BrowserTypeMiddleware>();
        app.UseMiddleware<ShortCircuitMiddleware>();
    }

    if(env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseStatusCodePages();
        app.UseBrowserLink();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
    }

    #region Depricated - 5/10/2018 Custom Middleware Unnecessary
    /*
                if(env.IsDevelopment())
{
    app.UseMiddleware<ErrorMiddleware>();
    app.UseMiddleware<BrowserTypeMiddleware>();
    app.UseMiddleware<ShortCircuitMiddleware>();
    app.UseMiddleware<ContentMiddleware>();
}

    */
        //#endregion

        //app.UseStaticFiles();
        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller=Home}/{action=Index}/{id?}");
        //    });

        //    #region Erik - 5/8/2018 Removed to show Middleware usage
        //    /*
        //    app.UseMvcWithDefaultRoute();
        //    */
        //    #endregion

        //    #region Depricated - 5/8/2018 Old Impl
        //    /*
        //     if (env.IsDevelopment())
        //{
        //    app.UseDeveloperExceptionPage();
        //}

        //app.Run(async (context) =>
        //{
        //    await context.Response.WriteAsync("Hello World!");
        //});
        //    */
        //    #endregion

        //}

        //        */
				#endregion

    }
}
