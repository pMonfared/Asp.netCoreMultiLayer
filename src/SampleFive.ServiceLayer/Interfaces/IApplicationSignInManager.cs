using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using SampleFive.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleFive.ServiceLayer.Interfaces
{
    public interface IApplicationSignInManager
    {
        Task<bool> CanSignInAsync(ApplicationUser user);
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null);
        Task<ClaimsPrincipal> CreateUserPrincipalAsync(ApplicationUser user);
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent);
        Task ForgetTwoFactorClientAsync();
        IEnumerable<AuthenticationDescription> GetExternalAuthenticationSchemes();
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null);
        Task<ApplicationUser> GetTwoFactorAuthenticationUserAsync();
        bool IsSignedIn(ClaimsPrincipal principal);
        Task<bool> IsTwoFactorClientRememberedAsync(ApplicationUser user);
        Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task RefreshSignInAsync(ApplicationUser user);
        Task RememberTwoFactorClientAsync(ApplicationUser user);
        Task SignInAsync(ApplicationUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null);
        Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null);
        Task SignOutAsync();
        Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberClient);
        Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin);
        Task<ApplicationUser> ValidateSecurityStampAsync(ClaimsPrincipal principal);
    }
}
