using CQRS_read_Infrastructure.Persistence.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS_read_Application.People
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public void Delete(int id)
        {
            personRepository.Delete(id);
        }

        public Person Find(int id)
        {
            return personRepository.Find(id);
        }

        public IQueryable<Person> GetAll()
        {
            return personRepository.Get();
        }

        public IQueryable<Person> GetByName(string nome)
        {
            return personRepository.Get(p => p.Nome.ToUpper().Contains(nome.ToUpper()));
        }

        public void Insert(Person person)
        {
            personRepository.Insert(person);
        }

        public void Update(Person person)
        {
            personRepository.Update(person);
        }
    }
}
