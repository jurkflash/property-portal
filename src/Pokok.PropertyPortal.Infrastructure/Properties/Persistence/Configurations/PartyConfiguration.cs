using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Parties.Aggregates;
using Pokok.PropertyPortal.Domain.Parties.Entities;
using Pokok.PropertyPortal.Domain.Parties.ValueObjects;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations
{
    public class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
               .HasConversion(
                    id => id.Value,
                    value => new PartyId(value) 
                );

            builder.OwnsOne(p => p.PartyName, pn =>
            {
                pn.Property(x => x.Value).HasColumnName(nameof(PartyName)).IsRequired();
            });

            builder.OwnsOne(p => p.Email, e =>
            {
                e.Property(x => x.Value).HasColumnName(nameof(Email)).IsRequired();
            });

            builder.OwnsOne(p => p.PhoneNumber, ph =>
            {
                ph.Property(x => x.Value).HasColumnName(nameof(PhoneNumber));
            });

            builder.Property(r => r.PartyType)
                   .HasConversion<int>()
                   .IsRequired();

            builder.ToTable("Parties");
        }
    }
}
