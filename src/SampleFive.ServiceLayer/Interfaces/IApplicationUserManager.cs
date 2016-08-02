
using Microsoft.AspNetCore.Identity;
using SampleFive.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SampleFive.ServiceLayer.Interfaces
{
    public interface IApplicationUserManager : IDisposable
    {
        bool SupportsQueryableUsers { get; }
        bool SupportsUserAuthenticationTokens { get; }
        bool SupportsUserClaim { get; }
        bool SupportsUserEmail { get; }
        bool SupportsUserLockout { get; }
        bool SupportsUserLogin { get; }
        bool SupportsUserPassword { get; }
        bool SupportsUserPhoneNumber { get; }
        bool SupportsUserRole { get; }
        bool SupportsUserSecurityStamp { get; }
        bool SupportsUserTwoFactor { get; }
        IQueryable<ApplicationUser> Users { get; }
        Task<IdentityResult> AccessFailedAsync(ApplicationUser user);
        
        Task<IdentityResult> AddClaimAsync(ApplicationUser user, Claim claim);
        
        Task<IdentityResult> AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims);
        
        Task<IdentityResult> AddLoginAsync(ApplicationUser user, UserLoginInfo login);
        
        Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password);
        
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        
        Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        
        Task<IdentityResult> ChangeEmailAsync(ApplicationUser user, string newEmail, string token);
        
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        Task<IdentityResult> ChangePhoneNumberAsync(ApplicationUser user, string phoneNumber, string token);
        
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
       
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token);
       
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        
        Task<IdentityResult> DeleteAsync(ApplicationUser user);

        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(string userId);
        Task<ApplicationUser> FindByLoginAsync(string loginProvider, string providerKey);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<string> GenerateChangeEmailTokenAsync(ApplicationUser user, string newEmail);
        Task<string> GenerateChangePhoneNumberTokenAsync(ApplicationUser user, string phoneNumber);
        Task<string> GenerateConcurrencyStampAsync(ApplicationUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<string> GenerateTwoFactorTokenAsync(ApplicationUser user, string tokenProvider);
        Task<string> GenerateUserTokenAsync(ApplicationUser user, string tokenProvider, string purpose);
        Task<int> GetAccessFailedCountAsync(ApplicationUser user);
        Task<string> GetAuthenticationTokenAsync(ApplicationUser user, string loginProvider, string tokenName);
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<string> GetEmailAsync(ApplicationUser user);
        Task<bool> GetLockoutEnabledAsync(ApplicationUser user);
        Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUser user);
        Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user);
        Task<string> GetPhoneNumberAsync(ApplicationUser user);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<string> GetSecurityStampAsync(ApplicationUser user);
        Task<bool> GetTwoFactorEnabledAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal);
        string GetUserId(ClaimsPrincipal principal);
        Task<string> GetUserIdAsync(ApplicationUser user);
        string GetUserName(ClaimsPrincipal principal);
        Task<string> GetUserNameAsync(ApplicationUser user);
        Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim);
        Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName);
        Task<IList<string>> GetValidTwoFactorProvidersAsync(ApplicationUser user);
        Task<bool> HasPasswordAsync(ApplicationUser user);
        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);
        Task<bool> IsInRoleAsync(ApplicationUser user, string role);
        Task<bool> IsLockedOutAsync(ApplicationUser user);
        Task<bool> IsPhoneNumberConfirmedAsync(ApplicationUser user);
        string NormalizeKey(string key);
        void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<ApplicationUser> provider);
        Task<IdentityResult> RemoveAuthenticationTokenAsync(ApplicationUser user, string loginProvider, string tokenName);
        Task<IdentityResult> RemoveClaimAsync(ApplicationUser user, Claim claim);
        Task<IdentityResult> RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims);
        Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string role);
        Task<IdentityResult> RemoveFromRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveLoginAsync(ApplicationUser user, string loginProvider, string providerKey);
        Task<IdentityResult> RemovePasswordAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken));
        Task<IdentityResult> ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim);
        Task<IdentityResult> ResetAccessFailedCountAsync(ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
        Task<IdentityResult> ResetPasswordAsync(string userId, string usedToken, string newPassword);
        Task<IdentityResult> SetAuthenticationTokenAsync(ApplicationUser user, string loginProvider, string tokenName, string tokenValue);
        Task<IdentityResult> SetEmailAsync(ApplicationUser user, string email);
        Task<IdentityResult> SetLockoutEnabledAsync(ApplicationUser user, bool enabled);
        Task<IdentityResult> SetLockoutEndDateAsync(ApplicationUser user, DateTimeOffset? lockoutEnd);
        Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber);
        Task<IdentityResult> SetTwoFactorEnabledAsync(ApplicationUser user, bool enabled);
        Task<IdentityResult> SetUserNameAsync(ApplicationUser user, string userName);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task UpdateNormalizedEmailAsync(ApplicationUser user);
        Task UpdateNormalizedUserNameAsync(ApplicationUser user);
        Task<IdentityResult> UpdateSecurityStampAsync(ApplicationUser user);
        Task<bool> VerifyChangePhoneNumberTokenAsync(ApplicationUser user, string token, string phoneNumber);
        Task<bool> VerifyTwoFactorTokenAsync(ApplicationUser user, string tokenProvider, string token);
        Task<bool> VerifyUserTokenAsync(ApplicationUser user, string tokenProvider, string purpose, string token);
    }
}
