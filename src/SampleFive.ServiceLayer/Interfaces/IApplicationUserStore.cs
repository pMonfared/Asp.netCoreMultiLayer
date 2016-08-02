using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using SampleFive.DomainLayer.Models;

namespace SampleFive.ServiceLayer.Interfaces
{
    public interface IApplicationUserStore : IUserStore<ApplicationUser>
    {
        Task<IdentityResult> AddToUsedPasswordAsync(ApplicationUser appuser, string userpassword, CancellationToken cancellationToken = default(CancellationToken));
    }
}