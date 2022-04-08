using CQRS_read_Application.People;
using CQRS_Write_Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.People
{
    public class PersonEventsHandlers : IEventHandler<PersonCreatedEvent>, IEventHandler<PersonDeletedEvent>
    {
        private readonly IPersonService personService;

        public PersonEventsHandlers(IPersonService personService)
        {
            this.personService = personService;
        }

        public void Handle(PersonDeletedEvent @event)
        {
            this.personService.Delete(@event.AggregateId);
        }

        public void Handle(PersonRenamedEvent @event)
        {
            var person = this.personService.Find(@event.AggregateId);
            person.Nome = @event.Nome;
            this.personService.Update(person);
        }

        public void Handle(PersonCreatedEvent @event)
        {
            CQRS_read_Infrastructure.Persistence.People.Person person =
               new CQRS_read_Infrastructure.Persistence.People.Person
               (@event.AggregateId,
               (CQRS_read_Infrastructure.Persistence.People.PersonClass)(@event.Class)
               , @event.Nome,
               @event.Idade);

            this.personService.Insert(person);
        }
    }
}
