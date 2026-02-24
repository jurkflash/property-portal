using Pokok.BuildingBlocks.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokok.PropertyPortal.Application.DomainEventHandlers
{
    public class PartyRegisteredEventHandler<TEvent> : IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        public Task Handle(TEvent domainEvent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
