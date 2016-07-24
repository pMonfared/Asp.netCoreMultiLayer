using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SampleFive.DomainLayer.Models
{
    //public class ApplicationUser: IdentityUser<int,ApplicationUserClaim,ApplicationUserRole,ApplicationUserLogin>
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    //public class ApplicationRole : IdentityRole<int, ApplicationUserRole, ApplicationRoleClaim>
    public class ApplicationRole : IdentityRole<int>
    {
        public string Description { get; set; }
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name)
            : this()
        {
            Name = name;
        }

        public ApplicationRole(string name, string description)
            : this(name)
        {
            this.Description = description;
        }

    }

    //public class ApplicationUserLogin : IdentityUserLogin<int>
    //{

    //}
    //public class ApplicationUserRole : IdentityUserRole<int>
    //{

    //}
    //public class ApplicationRoleClaim : IdentityRoleClaim<int>
    //{

    //}
    //public class ApplicationUserClaim : IdentityUserClaim<int>
    //{

    //}

    //public class ApplicationUserToken : IdentityUserToken<int>
    //{

    //}
}