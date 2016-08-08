using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleFive.DomainLayer.Models;
using SampleFive.FluentApiHelper;

namespace SampleFive.DomainLayer.Mappings
{
    public class ApplicationUserClaimMap : EntityMappingConfiguration<ApplicationUserClaim>
    {
        public override void Map(EntityTypeBuilder<ApplicationUserClaim> b)
        {
            b.ToTable("AppUserClaims", "HumanResources")
                .HasKey(p => p.Id);
        }
    }
}
