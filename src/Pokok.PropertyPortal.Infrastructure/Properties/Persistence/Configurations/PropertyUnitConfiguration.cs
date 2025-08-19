using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokok.PropertyPortal.Domain.Properties;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations
{
    public class PropertyUnitConfiguration : IEntityTypeConfiguration<PropertyUnit>
    {
        public void Configure(EntityTypeBuilder<PropertyUnit> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UnitNumber)
                .HasConversion(un => un.Value, v => new UnitNumber(v))
                .IsRequired();

            // One-to-many Unit -> Residents
            builder.HasMany(typeof(Resident), "_residents")
                   .WithOne()
                   .HasForeignKey("UnitId");
        }
    }
}
