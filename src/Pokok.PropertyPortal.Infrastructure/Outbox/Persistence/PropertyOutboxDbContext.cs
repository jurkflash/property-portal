using Microsoft.EntityFrameworkCore;
using Pokok.BuildingBlocks.Outbox;

namespace Pokok.PropertyPortal.Infrastructure.Outbox.Persistence
{
    public class PropertyOutboxDbContext : OutboxDbContext
    {
        public PropertyOutboxDbContext(DbContextOptions<PropertyOutboxDbContext> options)
            : base(options)
        {
        }
    }
}
