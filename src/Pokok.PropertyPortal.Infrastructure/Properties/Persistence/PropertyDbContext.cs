using Microsoft.EntityFrameworkCore;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Residents;
using Pokok.PropertyPortal.Infrastructure.Properties.Persistence.Configurations;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Persistence
{
    public class PropertyDbContext : DbContext
    {
        public PropertyDbContext(DbContextOptions<PropertyDbContext> options)
            : base(options)
        {
        }

        // Aggregate roots
        public DbSet<Property> Properties { get; set; } = default!;
        public DbSet<PropertyUnit> PropertyUnits { get; set; } = default!;
        public DbSet<Resident> Residents { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Property aggregate
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());

            // Configure Unit aggregate/entity
            modelBuilder.ApplyConfiguration(new PropertyUnitConfiguration());

            // Configure Resident aggregate/entity
            modelBuilder.ApplyConfiguration(new ResidentConfiguration());
        }
    }
}
