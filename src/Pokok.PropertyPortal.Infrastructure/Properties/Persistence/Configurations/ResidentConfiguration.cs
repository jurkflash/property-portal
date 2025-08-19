using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokok.PropertyPortal.Domain.Residents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations
{
    public class ResidentConfiguration : IEntityTypeConfiguration<Resident>
    {
        public void Configure(EntityTypeBuilder<Resident> builder)
        {
            builder.HasKey(r => r.Id);

            // Map PersonName value object
            builder.OwnsOne(r => r.Name, n =>
            {
                n.Property(p => p.FirstName).HasMaxLength(100).IsRequired();
                n.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            });

            // Map Email value object
            builder.OwnsOne(r => r.Email, e =>
            {
                e.Property(p => p.Value)
                 .HasColumnName("Email")
                 .HasMaxLength(200)
                 .IsRequired();
            });

            // Map PhoneNumber value object (optional)
            builder.OwnsOne(r => r.Phone, p =>
            {
                p.Property(ph => ph.Value)
                 .HasColumnName("Phone")
                 .HasMaxLength(50);
            });

            // Map Role (assuming enum or value object)
            builder.Property(r => r.Role)
                   .HasConversion<string>()   // stores enum as string
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
