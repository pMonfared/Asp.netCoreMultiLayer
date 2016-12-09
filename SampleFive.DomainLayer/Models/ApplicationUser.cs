using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SampleFive.DomainLayer.Models
{
    public class ApplicationUser : IdentityUser<int, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin>
    //public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            this.UserUsedPasswords = new List<ApplicationUserUsedPassword>();
        }
        public ICollection<ApplicationUserUsedPassword> UserUsedPasswords { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    //public class ApplicationRole : IdentityRole<int>
    //{
    //    public string Description { get; set; }
    //    public ApplicationRole()
    //    {
    //    }

    //    public ApplicationRole(string name)
    //        : this()
    //    {
    //        Name = name;
    //    }

    //    public ApplicationRole(string name, string description)
    //        : this(name)
    //    {
    //        this.Description = description;
    //    }
    //}
    public class ApplicationRole : IdentityRole<int, ApplicationUserRole, ApplicationRoleClaim>

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

    public class ApplicationUserLogin : IdentityUserLogin<int>
    {

    }
    public class ApplicationUserRole : IdentityUserRole<int>
    {

    }
    public class ApplicationRoleClaim : IdentityRoleClaim<int>
    {

    }
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {

    }

    public class ApplicationUserToken : IdentityUserToken<int>
    {

    }

    public class ApplicationUserUsedPassword : BaseEntity
    {
        public ApplicationUserUsedPassword()
        {
            CreatedDate = DateTimeOffset.UtcNow;
        }

        [Key, Column(Order = 0)]
        public string HashPassword { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        [Key, Column(Order = 1)]
        public int UserId { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
    }
}