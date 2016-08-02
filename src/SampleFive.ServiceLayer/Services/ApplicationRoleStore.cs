using System;
using System.Threading;
using System.Threading.Tasks;
using SampleFive.DomainLayer.Models;
using SampleFive.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace SampleFive.ServiceLayer.Services
{
    public class ApplicationRoleStore : IApplicationRoleStore
    {
        private readonly IRoleStore<ApplicationRole> _roleStore;

        public ApplicationRoleStore(IRoleStore<ApplicationRole> roleStore)
        {
            _roleStore = roleStore;
        }

        public Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
           return _roleStore.CreateAsync(role, cancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return _roleStore.DeleteAsync(role, cancellationToken);
        }

        public void Dispose()
        {
            _roleStore.Dispose();
        }

        public Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return _roleStore.FindByIdAsync(roleId, cancellationToken);
        }

        public Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return _roleStore.FindByNameAsync(normalizedRoleName, cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return _roleStore.GetNormalizedRoleNameAsync(role, cancellationToken);
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return _roleStore.GetRoleIdAsync(role, cancellationToken);
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return _roleStore.GetRoleNameAsync(role, cancellationToken);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            return _roleStore.SetNormalizedRoleNameAsync(role,normalizedName, cancellationToken);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            return _roleStore.SetRoleNameAsync(role, roleName, cancellationToken);
        }

        public Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return _roleStore.UpdateAsync(role, cancellationToken);
        }
    }
}