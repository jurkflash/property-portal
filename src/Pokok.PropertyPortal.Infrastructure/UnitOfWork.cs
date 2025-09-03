using Pokok.BuildingBlocks.Cqrs.Events;
using Pokok.BuildingBlocks.Persistence.Base;
using Pokok.PropertyPortal.Infrastructure.Properties.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokok.PropertyPortal.Infrastructure
{
    public class UnitOfWork : UnitOfWorkBase
    {
        public UnitOfWork(PropertyDbContext context, IDomainEventDispatcher dispatcher)
            : base(context, dispatcher) { }
    }
}
