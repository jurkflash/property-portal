using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
               .HasConversion(
                   id => id.Value,
                   value => new PropertyId(value));

            // Map PropertyName value object
            builder.OwnsOne(p => p.PropertyName, pn =>
            {
                pn.Property(p => p.Value)
                  .HasColumnName(nameof(PropertyName))
                  .IsRequired()
                  .HasMaxLength(500);
            });

            // Map Address value object
            builder.OwnsOne(p => p.Address, a =>
            {
                a.Property(p => p.Street).HasMaxLength(200).IsRequired();
                a.Property(p => p.City).HasMaxLength(100).IsRequired();
                a.Property(p => p.State).HasMaxLength(100);
                a.Property(p => p.PostalCode).HasMaxLength(20);
                a.Property(p => p.Country).HasMaxLength(100);
            });

            builder.HasMany(p => p.Units)
               .WithOne()
               .HasForeignKey("PropertyId")
               .OnDelete(DeleteBehavior.Cascade);

            // Optional: map table name
            builder.ToTable("Properties");
        }
    }
}
