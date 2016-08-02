using SampleFive.DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using SampleFive.ServiceLayer.Interfaces;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace SampleFive.ServiceLayer.Services
{
    public class ApplicationUserStore : IApplicationUserStore
    {
        private readonly IUserStore<ApplicationUser> _userStore;

        public ApplicationUserStore(IUserStore<ApplicationUser> userStore)
        {
            _userStore = userStore;
        }

        #region ##Base Methods
        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetUserIdAsync(user, cancellationToken);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetUserNameAsync(user, cancellationToken);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            return _userStore.SetUserNameAsync(user, userName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return _userStore.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.CreateAsync(user, cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.UpdateAsync(user, cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return _userStore.DeleteAsync(user, cancellationToken);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userStore.FindByIdAsync(userId, cancellationToken);
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userStore.FindByNameAsync(normalizedUserName, cancellationToken);
        }
        public void Dispose()
        {
            _userStore.Dispose();
        }
        #endregion

        #region #Custom Methods
        public Task<IdentityResult> AddToUsedPasswordAsync(ApplicationUser appuser, string userpassword, CancellationToken cancellationToken)
        {
            appuser.UserUsedPasswords.Add(new ApplicationUserUsedPassword { UserId = appuser.Id, HashPassword = userpassword });
            return _userStore.UpdateAsync(appuser, cancellationToken);
        }
        #endregion
    }
}
