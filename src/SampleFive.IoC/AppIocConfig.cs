using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleFive.DataLayer.Context;
using SampleFive.DomainLayer.Models;
using SampleFive.ServiceLayer.Interfaces;
using SampleFive.ServiceLayer.Services;
using StructureMap;

namespace SampleFive.IoC
{
    public static class AppIocConfig
    {
        public static IServiceProvider IocConfig(IServiceCollection services, IConfigurationRoot configuration, IHttpContextAccessor httpContextAccessor)
        {
            var container = new Container();
            container.Configure(config =>
            {
                config.For<IConfigurationRoot>().Singleton().Use(() => configuration);
                config.For<IHttpContextAccessor>().Singleton().Use(() => httpContextAccessor);
                config.For<IUnitOfWork>().ContainerScoped().Use<ApplicationDbContext>();
                config.For<ApplicationDbContext>().ContainerScoped().Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());

                #region #SysB UserMg

                // map same interface to different concrete classes

                config.For<IUserStore<ApplicationUser>>().ContainerScoped()
                .Use<UserStore<ApplicationUser,ApplicationRole,ApplicationDbContext,int,ApplicationUserClaim,ApplicationUserRole,ApplicationUserLogin,ApplicationUserToken>>();

                //config.For<IAuthenticationManager>().Use(() =>  HttpContext.Current.GetOwinContext().Authentication);
                config.For<IApplicationUserManager>().ContainerScoped().Use<ApplicationUserManager>();
                config.For<ApplicationUserManager>().ContainerScoped().Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());
                config.For<IApplicationRoleStore>().ContainerScoped().Use<ApplicationRoleStore>();
                config.For<IApplicationUserStore>().ContainerScoped().Use<ApplicationUserStore>();
                config.For<IApplicationSignInManager>().ContainerScoped().Use<ApplicationSignInManager>();
                config.For<IApplicationRoleManager>().ContainerScoped().Use<ApplicationRoleManager>();

                config.For<IEmailSender>().Use<AuthMessageSender>();
                config.For<ISmsSender>().Use<AuthMessageSender>();
                #endregion

                config.Scan(_ =>
                {
                    _.AssemblyContainingType<ISettingService>();
                    _.WithDefaultConventions();
                });

            });

            container.Populate(services);

            return container.GetInstance<IServiceProvider>();
        }
    }
}