using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.People
{
    public class Person : AggregateRoot<int>
    {
        public PersonType Class { get; protected set; }

        public Person() { }

        public Person(int id, PersonClass personClass, string nome, int idade)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentNullException("nome");

            this.ApplyChange(new PersonCreatedEvent(id, personClass, nome, idade));
        }

        public Person(PersonClass personClass, string nome, int idade)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentNullException("nome");

            this.ApplyChange(new PersonCreatedEvent(0, personClass, nome, idade));
        }
        public void Rename(string name)
        {
            this.ApplyChange(new PersonRenamedEvent(this.Id, name));
        }

        public void Delete()
        {
            this.ApplyChange(new PersonDeletedEvent(this.Id));
        }

        private void Apply(PersonRenamedEvent @event)
        {
            this.Id = @event.AggregateId;
            this.Class = new PersonType(this.Class.Class, @event.Nome, this.Class.Idade);
        }

        public override string ToString()
        {
            return $"{this.Class.Nome}: [Class]{this.Class}, [Nome]{Class.Nome}, [Idade]{Class.Idade}";
        }

    }
}
