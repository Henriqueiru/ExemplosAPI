using CQRS_Write_Domain;
using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Infrastructure.Commands
{
    public class CommandEventRepository : ICommandEventRepository
    {

        private IEventPublisher eventPublisher;

        private Dictionary<object, List<IEvent>> aggregateEventsDictonary = new Dictionary<object, List<IEvent>>();


        public CommandEventRepository(IEventPublisher eventPublisher)
        { this.eventPublisher = eventPublisher; }


        public T GetByCommandId<T>(object aggregateId) where T : IAggregateRoot
        {
            T aggregate = (T)Activator.CreateInstance(typeof(T));

            List<IEvent> aggregateEvents;
            if (aggregateEventsDictonary.TryGetValue(aggregateId, out aggregateEvents))
            {
                aggregate.LoadsFromHistory(aggregateEvents);

                return aggregate;
            }

            return default(T);

        }

        public IEnumerable<IEvent> GetEvents(object aggregateId)
        {
            List<IEvent> aggregateEvents;
            if (aggregateEventsDictonary.TryGetValue(aggregateId, out aggregateEvents))
            {
                return aggregateEvents;
            }

            return new List<IEvent>();
        }

        public void Save(IAggregateRoot aggregate)
        {
            List<IEvent> aggregateEvents;
            if (!aggregateEventsDictonary.TryGetValue(aggregate.GetId(), out aggregateEvents))
            {
                aggregateEvents = new List<IEvent>();
                aggregateEventsDictonary.Add(aggregate.GetId(), aggregateEvents);
            }

            // Verifica se a versão mais recente do evento corresponde à versão agregada atual

            foreach (var @event in aggregate.GetUncommittedChanges())
            {
                aggregateEvents.Add(@event);
                this.eventPublisher.Publish(@event);
            }

            aggregate.MarkChangesAsCommitted();
        }
    }
}
