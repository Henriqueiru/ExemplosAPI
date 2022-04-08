using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.Events
{
    public interface IEvent
    {
        long Timestamp { get; set; }
        int AggregateId { get; set; }
        string Type { get; }
        int Version { get; set; }
    }
}
