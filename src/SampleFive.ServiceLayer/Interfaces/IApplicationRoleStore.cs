using SampleFive.DomainLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace SampleFive.ServiceLayer.Interfaces
{
    public interface IApplicationRoleStore : IRoleStore<ApplicationRole>
    {
    }
}