using CQRS_read_Infrastructure.Persistence.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_read_Infrastructure.Persistence
{
    public class Contexto : IContexto
    {
        public IPersonRepository People { get; set; }
        public Contexto(IPersonRepository personRepository)
        {
            this.People = personRepository;
        }


    }
}
