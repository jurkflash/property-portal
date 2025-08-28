using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations
{
    public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
    {
        public void Configure(EntityTypeBuilder<Resident> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
               .HasConversion(
                    id => id.Value,
                    value => new ResidentId(value)
                );

            builder.OwnsOne(r => r.PhoneNumber, r =>
            {
                r.Property(r => r.Value)
                  .HasColumnName(nameof(PhoneNumber))
                  .IsRequired()
                  .HasMaxLength(50);
            });

            builder.Property(r => r.ResidentRole)
                   .HasConversion<int>()
                   .IsRequired();

            // Resident owns a Party reference, store PartyId as FK
            builder.HasOne(r => r.Party)
                   .WithMany()
                   .HasForeignKey("PartyId")
                   .IsRequired();

            builder.ToTable("Residents");
        }
    }
}
