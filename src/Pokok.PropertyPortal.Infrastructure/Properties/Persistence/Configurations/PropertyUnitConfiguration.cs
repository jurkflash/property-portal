using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokok.PropertyPortal.Domain.Properties.Entities;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations
{
    public class PropertyUnitConfiguration : IEntityTypeConfiguration<PropertyUnit>
    {
        public void Configure(EntityTypeBuilder<PropertyUnit> builder)
        {
            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.UnitNumber, un =>
            {
                un.Property(x => x.Value).HasColumnName("UnitNumber").IsRequired();
            });

            builder.HasMany(u => u.Residents)
                   .WithOne()
                   .HasForeignKey("UnitId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PropertyUnits");
        }
    }
}
