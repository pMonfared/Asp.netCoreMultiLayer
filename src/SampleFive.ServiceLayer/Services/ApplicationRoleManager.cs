using SampleFive.DomainLayer.Models;
using SampleFive.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace SampleFive.ServiceLayer.Services
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>, IApplicationRoleManager
    {
        private readonly IRoleStore<ApplicationRole> _roleStore;
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators, 
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, 
            ILogger<RoleManager<ApplicationRole>> logger, IHttpContextAccessor contextAccessor) 
            : base(store, roleValidators, keyNormalizer, errors, logger, contextAccessor)
        {
            _roleStore = store;
        }

        
    }
}
