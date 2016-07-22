using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SampleFive.ServiceLayer;
using SampleFive.PresentaionLayer;
using StructureMap;
using Microsoft.Extensions.FileProviders;
using SampleFive.StartupCustomizations;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Reflection;
using System.IO;

namespace SampleFive
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

        public IConfigurationRoot Configuration { set; get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddInMemoryCollection(new[]
                                {
                                    new KeyValuePair<string,string>("the-key", "the-value"),
                                })
                            .AddJsonFile("appsettings.json", reloadOnChange: true, optional: false)
                            .AddJsonFile($"appsettings.{env}.json", optional: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<SmtpConfig>(options => Configuration.GetSection("Smtp").Bind(options));
            services.AddDirectoryBrowser();
            services.AddMvc().AddControllersAsServices();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());
            });
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new FeatureLocationExpander());

                var thisAssembly = typeof(Startup).GetTypeInfo().Assembly;
                options.FileProviders.Clear();
                options.FileProviders.Add(new EmbeddedFileProvider(thisAssembly, baseNamespace: "SampleFive"));
            });

            //Dependency Injection with ASP.net Core DI
            //services.AddSingleton<IConfigurationRoot>(provider => { return Configuration; });
            //services.AddTransient<IMessagesService, MessagesService>();


            //Dependency Injection with StructureMap.DNX
            return IocConfig(services);
        }

        private IServiceProvider IocConfig(IServiceCollection services)
        {
            var container = new Container();
            container.Configure(config =>
            {
                config.For<IConfigurationRoot>().Singleton().Use(() => Configuration);
                config.Scan(_ =>
                {
                    _.AssemblyContainingType<IMessagesService>();
                    _.WithDefaultConventions();
                });
                config.Populate(services);
            });

            container.Populate(services);

            return container.GetInstance<IServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,IMessagesService messagesService)
        {

            

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/MyControllerName/SomeActionMethodName/{0}");
                app.UseExceptionHandler(errorHandlingPath: "/MyControllerName/SomeActionMethodName");
            }


            app.UseDefaultFiles();
            app.UseStaticFiles(); // For the wwwroot folder

            // For the files outside of the wwwroot
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(root: Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });

            // For DirectoryBrowser
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(root: Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\content\images")),
                RequestPath = new PathString("/MyImages")
            });

            app.UseFileServer();
            app.UseFileServer(new FileServerOptions
            {
                // Set root of file server
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                // Only react to requests that match this path
                RequestPath = "/node_modules",
                // Don't expose file system
                EnableDirectoryBrowsing = false
            });

            // Serve /bower_components as a separate root
            app.UseFileServer(new FileServerOptions
            {
                // Set root of file server
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\lib")),
                // Only react to requests that match this path
                RequestPath = "/scripts",
                // Don't expose file system
                EnableDirectoryBrowsing = false
            });
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "default",
                 template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });

            app.Run(async (context) =>
            {
                var siteName = messagesService.GetSiteName2();
                await context.Response.WriteAsync($"Kingdom {siteName}");
            });
        }
    }
}
