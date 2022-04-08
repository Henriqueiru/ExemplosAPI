using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.People
{
    [Flags]
    public enum PersonClass
    {
        Comum,
        Admin
    }
}
