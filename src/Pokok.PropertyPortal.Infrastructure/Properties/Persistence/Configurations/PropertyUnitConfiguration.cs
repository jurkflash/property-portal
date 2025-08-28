using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations
{
    public class PropertyUnitConfiguration : IEntityTypeConfiguration<PropertyUnit>
    {
        public void Configure(EntityTypeBuilder<PropertyUnit> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(p => p.Id)
               .HasConversion(
                    id => id.Value,
                    value => new PropertyUnitId(value)
                );

            builder.OwnsOne(u => u.UnitNumber, un =>
            {
                un.Property(x => x.Value).HasColumnName(nameof(UnitNumber)).IsRequired();
            });

            builder.HasMany(u => u.Residents)
                   .WithOne()
                   .HasForeignKey(nameof(PropertyUnitId))
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PropertyUnits");
        }
    }
}
