using SampleFive.DomainLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SampleFive.ServiceLayer.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace SampleFive.ServiceLayer.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>, IApplicationUserManager
    {
        private const int UsedPasswordLimit = 5;
        private readonly IUserStore<ApplicationUser> _userStore;
        IPasswordHasher<ApplicationUser> _passwordHasher;
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _userStore= store;
            _passwordHasher = passwordHasher;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await base.CreateAsync(user);
            if (result.Succeeded)
                await AddToUsedPasswordAsync(user, user.PasswordHash);
            return result;
        }



        #region #Control Change Passwords

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            if (await IsPreviousPassword(userId, newPassword))
            {
                return await Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "نمیتوانید از کلمه عبور قبلی خود دوباره استفاده نمایید" }));
            }
            var appuser = await base.FindByIdAsync(userId);
            var result = await base.ChangePasswordAsync(appuser, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await this.AddToUsedPasswordAsync(await FindByIdAsync(userId), _passwordHasher.HashPassword(appuser,newPassword));
            }
            return result;
        }

        public override async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            if (IsPreviousPassword(user, newPassword))
            {
                return await Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "نمیتوانید از کلمه عبور قبلی خود دوباره استفاده نمایید" }));
            }

            var result = await base.ChangePasswordAsync(user, currentPassword, newPassword);
            if (result.Succeeded)
            {
                await this.AddToUsedPasswordAsync(user, _passwordHasher.HashPassword(user, newPassword));
            }
            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(string userId, string usedToken, string newPassword)
        {

            if (await IsPreviousPassword(userId, newPassword))
            {
                return await Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "نمیتوانید از کلمه عبور قبلی خود دوباره استفاده نمایید" }));
            }
            var appuser = await base.FindByIdAsync(userId);
            var result = await base.ResetPasswordAsync(appuser, usedToken, newPassword);
            if (result.Succeeded)
            {
                await this.AddToUsedPasswordAsync(await FindByIdAsync(userId), _passwordHasher.HashPassword(appuser, newPassword));
            }

            return result;
        }

        public override async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string usedToken, string newPassword)
        {

            if (IsPreviousPassword(user, newPassword))
            {
                return await Task.FromResult(IdentityResult.Failed(new IdentityError { Description= "نمیتوانید از کلمه عبور قبلی خود دوباره استفاده نمایید" }));
            }
            var result = await base.ResetPasswordAsync(user, usedToken, newPassword);
            if (result.Succeeded)
            {
                await this.AddToUsedPasswordAsync(user, _passwordHasher.HashPassword(user, newPassword));
            }

            return result;
        }

        private Task AddToUsedPasswordAsync(ApplicationUser appuser, string userpassword)
        {
            appuser.UserUsedPasswords.Add(new ApplicationUserUsedPassword { UserId = appuser.Id, HashPassword = userpassword });
            return _userStore.UpdateAsync(appuser, cancellationToken : default(CancellationToken));
        }


        private async Task<bool> IsPreviousPassword(string userId, string newPassword)
        {
            var user = await FindByIdAsync(userId);
            return
                user.UserUsedPasswords
                    .OrderByDescending(up => up.CreatedDate)
                    .Select(up => up.HashPassword)
                    .Take(UsedPasswordLimit)
                    .Any(up => _passwordHasher.VerifyHashedPassword(user,up, newPassword) != PasswordVerificationResult.Failed);
        }

        private bool IsPreviousPassword(ApplicationUser user, string newPassword)
        {
            return
                user.UserUsedPasswords
                    .OrderByDescending(up => up.CreatedDate)
                    .Select(up => up.HashPassword)
                    .Take(UsedPasswordLimit)
                    .Any(up => _passwordHasher.VerifyHashedPassword(user, up, newPassword) != PasswordVerificationResult.Failed);
        }

        #endregion

    }
}
