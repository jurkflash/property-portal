using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokok.PropertyPortal.Domain.Parties.Aggregates;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations
{
    public class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.PartyName, pn =>
            {
                pn.Property(x => x.Value).HasColumnName("PartyName").IsRequired();
            });

            builder.OwnsOne(p => p.Email, e =>
            {
                e.Property(x => x.Value).HasColumnName("Email").IsRequired();
            });

            builder.OwnsOne(p => p.Phone, ph =>
            {
                ph.Property(x => x.Value).HasColumnName("Phone");
            });

            builder.Property<int>("PartyType")
                   .HasConversion<int>()
                   .IsRequired();

            builder.ToTable("Parties");
        }
    }
}
