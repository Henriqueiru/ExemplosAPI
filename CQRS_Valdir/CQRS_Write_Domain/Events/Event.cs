using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.Events
{
    public class Event : IEvent
    {
        public long Timestamp { get; set; }
        public int AggregateId { get; set; }
        public string Type { get { return this.GetType().Name; } }
        public int Version { get; set; }
    }
}
