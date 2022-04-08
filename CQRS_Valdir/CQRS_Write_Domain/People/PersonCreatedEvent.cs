using CQRS_Write_Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.People
{
   public  class PersonCreatedEvent : Event
    {
        public PersonClass Class { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }
        public PersonCreatedEvent(int aggregateId, PersonClass personClass, string nome, int idade) : base()
        {
            this.AggregateId = aggregateId;
            this.Class = personClass;
            this.Nome = nome;
            this.Idade = idade;

        }
    }
}
