using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SampleFive.DomainLayer.Models
{
    public class ApplicationUser : IdentityUser<string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin>
    {
        public ApplicationUser()
        {
            this.UserUsedPasswords = new List<ApplicationUserUsedPassword>();
        }
        public ICollection<ApplicationUserUsedPassword> UserUsedPasswords { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ApplicationRole : IdentityRole<string, ApplicationUserRole, ApplicationRoleClaim>
    
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

    public class ApplicationUserLogin : IdentityUserLogin<string>
    {

    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {

    }
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {

    }
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {

    }

    public class ApplicationUserToken : IdentityUserToken<string>
    {

    }

    public class ApplicationUserUsedPassword
    {
        public ApplicationUserUsedPassword()
        {
            CreatedDate = DateTimeOffset.UtcNow;
        }
        [Key]
        public int Id { get; set; }

        [Key, Column(Order = 0)]
        public string HashPassword { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        [Key, Column(Order = 1)]
        public string UserId { get; set; }
        public virtual ApplicationUser AppUser { get; set; }
    }
}