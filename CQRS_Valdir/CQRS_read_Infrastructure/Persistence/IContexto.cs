using CQRS_read_Infrastructure.Persistence.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_read_Infrastructure.Persistence
{
    public interface IContexto
    {
        IPersonRepository People { get; set; }
    }
}
