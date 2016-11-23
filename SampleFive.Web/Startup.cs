using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleFive.PresentaionLayer;
using SampleFive.DataLayer.Context;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.IO.Compression;
using SampleFive.ServiceLayer;
using SampleFive.ServiceLayer.Services;
using System.Reflection;
using SampleFive.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc.Razor;
using SampleFive.DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using SampleFive.Web.StartupCustomizations;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using SampleFive.IoC;

namespace SampleFive.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

        public IConfigurationRoot Configuration { set; get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }


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
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("fa-IR"),
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


            // Use a MS SQL Server database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("SampleFive.DataLayer")));


            // Use a SQLite database
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlite(
            //        Configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly("SampleFive.DataLayer")
            //    )
            //);


            // Use a PostgreSQL database
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(
            //        Configuration.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly("SampleFive.DataLayer")
            //    )
            //);

            services.AddSession();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext, int>()
                .AddDefaultTokenProviders();

            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //    .AddUserStore<ApplicationUserStore>()
            //    .AddUserManager<ApplicationUserManager>()
            //    .AddRoleStore<ApplicationRoleManager>()
            //    .AddRoleManager<ApplicationRoleManager>()
            //    .AddDefaultTokenProviders();

            services.AddDirectoryBrowser();

            //Localization service settings
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionLoggingFilterAttribute));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", new MediaTypeHeaderValue("application/xml"));

            }).AddControllersAsServices()
              .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
              .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        return factory.Create(
                         baseName: type.FullName /* بر این اساس نام فایل منبع متناظر باید به همراه ذکر فضای نام پایه آن هم باشد */,
                         location: "SampleFive.ExternalResources" /*نام اسمبلی ثالث*/);
                    };
                }).AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });


            services.Configure<RazorViewEngineOptions>(options =>
            {
                //options.ViewLocationExpanders.Add(new FeatureLocationExpander());
                var thisAssembly = typeof(Startup).GetTypeInfo().Assembly;
                //options.FileProviders.Clear();
                options.FileProviders.Add(new EmbeddedFileProvider(thisAssembly, baseNamespace: "SampleFive"));
            });

            //Dependency Injection with ASP.net Core DI
            //services.AddSingleton<IConfigurationRoot>(provider => { return Configuration; });
            //services.AddTransient<IMessagesSampleService, MessagesSampleService>();
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            //Add Response compression services
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            //Dependency Injection with StructureMap.DNX
            return AppIocConfig.IocConfig(services, Configuration, HttpContextAccessor);
        }

        

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ISettingService messagesService)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(minLevel: LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseStatusCodePagesWithReExecute("/MyControllerName/SomeActionMethodName/{0}");
                //app.UseExceptionHandler(errorHandlingPath: "/MyControllerName/SomeActionMethodName");
            }

            app.UseSession(options: new SessionOptions
            {
                IdleTimeout = TimeSpan.FromMinutes(30),
                CookieName = ".SampleFive"
            });

            app.UseResponseCompression();
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

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("fa-IR")),
                SupportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("fa-IR")
                },
                SupportedUICultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("fa-IR")
                }
            });

            app.UseIdentity();


            //app.UseMvcWithDefaultRoute();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "default",
                 template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "login",
                    template: "login",
                    defaults: new { controller = "Account", action = "Login" });
                routes.MapRoute(
                    name: "register",
                    template: "register",
                    defaults: new { controller = "Account", action = "register" });

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
