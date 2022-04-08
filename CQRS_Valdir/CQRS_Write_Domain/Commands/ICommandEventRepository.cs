using CQRS_Write_Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.Commands
{
    public  interface ICommandEventRepository
    {
        void Save(IAggregateRoot aggregate);
        T GetByCommandId<T>(object aggregateId) where T : IAggregateRoot;
        IEnumerable<IEvent> GetEvents(object aggregateId);
    }
}
