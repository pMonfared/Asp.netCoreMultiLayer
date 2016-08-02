using Microsoft.AspNetCore.Identity;
using SampleFive.DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleFive.ServiceLayer.Interfaces
{
    public interface IApplicationRoleManager : IDisposable
    {
        IQueryable<ApplicationRole> Roles { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }
        Task<IdentityResult> AddClaimAsync(ApplicationRole role, Claim claim);
        Task<IdentityResult> CreateAsync(ApplicationRole role);
        Task<IdentityResult> DeleteAsync(ApplicationRole role);
        Task<ApplicationRole> FindByIdAsync(string roleId);
        Task<ApplicationRole> FindByNameAsync(string roleName);
        Task<IList<Claim>> GetClaimsAsync(ApplicationRole role);
        Task<string> GetRoleIdAsync(ApplicationRole role);
        Task<string> GetRoleNameAsync(ApplicationRole role);
        string NormalizeKey(string key);
        Task<IdentityResult> RemoveClaimAsync(ApplicationRole role, Claim claim);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> SetRoleNameAsync(ApplicationRole role, string name);
        Task<IdentityResult> UpdateAsync(ApplicationRole role);
        Task UpdateNormalizedRoleNameAsync(ApplicationRole role);
    }
}
