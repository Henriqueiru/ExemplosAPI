using CQRS_read_Infrastructure.Persistence.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS_read_Application.People
{
    public interface IPersonService : IApplicationService<Person>
    {
        Person Find(int id);
        IQueryable<Person> GetByName(string nome);
        IQueryable<Person> GetAll();
        void Insert(Person person);
        void Update(Person person);
        void Delete(int id);
    }
}
