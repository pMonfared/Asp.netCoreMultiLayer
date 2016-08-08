using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleFive.DomainLayer.Models;
using SampleFive.FluentApiHelper;
using Microsoft.EntityFrameworkCore;

namespace SampleFive.DomainLayer.Mappings
{
    public class ApplicationRoleMap : EntityMappingConfiguration<ApplicationRole>
    {
        public override void Map(EntityTypeBuilder<ApplicationRole> b)
        {
            b.ToTable("AppRoles", "HumanResources")
                .HasKey(p => p.Id);

            b.Property(t => t.Name).HasColumnName("Name");
            b.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
