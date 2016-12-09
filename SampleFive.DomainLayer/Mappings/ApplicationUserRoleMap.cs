using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleFive.DomainLayer.Models;
using SampleFive.FluentApiHelper;

namespace SampleFive.DomainLayer.Mappings
{
    public class ApplicationUserRoleMap : EntityMappingConfiguration<ApplicationUserRole>
    {
        public override void Map(EntityTypeBuilder<ApplicationUserRole> b)
        {
            b.ToTable("AppUserRoles", "HumanResources");
        }
    }
}
