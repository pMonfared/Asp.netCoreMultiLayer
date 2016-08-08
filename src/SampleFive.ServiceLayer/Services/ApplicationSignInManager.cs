using SampleFive.DomainLayer.Models;
using SampleFive.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SampleFive.ServiceLayer.Services
{
    public class ApplicationSignInManager :
        SignInManager<ApplicationUser>, IApplicationSignInManager
    {
        private readonly ApplicationUserManager _userManager;
        public ApplicationSignInManager(ApplicationUserManager userManager, 
            IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger) 
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
            _userManager = userManager;
        }


    }
}
