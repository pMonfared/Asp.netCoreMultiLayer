using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleFive.DomainLayer.Models;
using SampleFive.FluentApiHelper;

namespace SampleFive.DomainLayer.Mappings
{
    public class ApplicationUserLoginMap : EntityMappingConfiguration<ApplicationUserLogin>
    {
        public override void Map(EntityTypeBuilder<ApplicationUserLogin> b)
        {
            b.ToTable("AppUserLogins", "HumanResources");
        }
    }
}
