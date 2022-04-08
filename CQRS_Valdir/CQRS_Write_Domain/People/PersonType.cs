using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.People
{
    public struct PersonType
    {
        public PersonClass Class { get; private set; }

        public string Nome { get; private set; }

        public int Idade { get; private set; }

        public PersonType(PersonClass personClass, String nome, int idade)
        {
            this.Class = personClass;
            this.Nome = nome;
            this.Idade = idade;
        }
    }
}
