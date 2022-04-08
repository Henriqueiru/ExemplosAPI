using CQRS_Write_Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.People
{
    public class PersonDeletedEvent : Event
    {
        public PersonDeletedEvent(int aggregatiId) : base()
        {
            this.AggregateId = aggregatiId;
        }

    }
}
