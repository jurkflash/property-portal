using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations
{
    public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
    {
        public void Configure(EntityTypeBuilder<Resident> builder)
        {
            builder.HasKey(r => r.Id);

            // Resident owns a Party reference, store PartyId as FK
            builder.HasOne(r => r.Party)
                   .WithMany()
                   .HasForeignKey("PartyId")
                   .IsRequired();

            builder.Property<int>("Role") // assuming ResidentRole is enum
                   .HasConversion<int>()
                   .IsRequired();

            builder.ToTable("Residents");
        }
    }
}
