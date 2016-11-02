using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NoJS.Library;
using NoJS.Library.Common;
using NoJS.Library.Interfaces;
using NoJS.Library.Filters;
using NoJS.Library.Objects;

namespace NoJS.Tests.MVC {
    public class Startup {
        public Startup(IHostingEnvironment env) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();            
            services.AddTransient<ISitePreferenceRepository, SitePreferenceRepository>();
            services.AddDeviceSwitcher<UrlSwitcher>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            app.UseDeveloperExceptionPage();
            
            app.UseStaticFiles();

            app.UseLegacyMiddleware(new Options {EnableCSS = false, EnableRenderTime = true, EnableJS = false});

            app.UseMvc(routes => {
                routes.MapDeviceSwitcher();

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}