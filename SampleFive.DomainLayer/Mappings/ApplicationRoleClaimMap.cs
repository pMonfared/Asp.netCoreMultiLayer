using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleFive.DomainLayer.Models;
using SampleFive.FluentApiHelper;

namespace SampleFive.DomainLayer.Mappings
{
    public class ApplicationRoleClaimMap : EntityMappingConfiguration<ApplicationRoleClaim>
    {
        public override void Map(EntityTypeBuilder<ApplicationRoleClaim> b)
        {
            b.ToTable("AppRoleClaims", "HumanResources")
                .HasKey(p => p.Id);
        }
    }
}
