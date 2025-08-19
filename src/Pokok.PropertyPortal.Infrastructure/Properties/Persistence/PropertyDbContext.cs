using Microsoft.EntityFrameworkCore;
using Pokok.PropertyPortal.Domain.Parties;
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

        public DbSet<Property> Properties => Set<Property>();
        public DbSet<PropertyUnit> PropertyUnits => Set<PropertyUnit>();
        public DbSet<Resident> Residents => Set<Resident>();
        public DbSet<Party> Parties => Set<Party>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyUnitConfiguration());
            modelBuilder.ApplyConfiguration(new ResidentConfiguration());
            modelBuilder.ApplyConfiguration(new PartyConfiguration());
        }
    }
}
