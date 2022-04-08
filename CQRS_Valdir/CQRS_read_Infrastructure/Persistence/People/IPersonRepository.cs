using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_read_Infrastructure.Persistence.People
{
    public interface IPersonRepository : IRepository<Person>
    {
    }
}
