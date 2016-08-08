using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleFive.DomainLayer.Models;
using SampleFive.FluentApiHelper;

namespace SampleFive.DomainLayer.Mappings
{
    public class ApplicationUserUsedPasswordMap : EntityMappingConfiguration<ApplicationUserUsedPassword>
    {
        public override void Map(EntityTypeBuilder<ApplicationUserUsedPassword> b)
        {
            b.ToTable("ApplicationUserUsedPasswords", "HumanResources")
                .HasKey(p => p.Id);

            b.Property(p => p.HashPassword).IsRequired();
            b.Property(p => p.CreatedDate).IsRequired();
            b.Property(p => p.UserId).IsRequired();

            b.Property(t => t.HashPassword).HasColumnName("HashPassword");
            b.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            b.Property(t => t.UserId).HasColumnName("UserId");
            b.Ignore(t => t.CreatedDateTime);
            b.Ignore(t => t.EditedDateTime);
            b.Ignore(t => t.CreatedUserId);
            b.Ignore(t => t.EditedUserId);

            b.HasOne(t => t.AppUser)
                .WithMany(t => t.UserUsedPasswords)
                .HasForeignKey(d => d.UserId);
        }
    }
}
