using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.Commands
{
    public interface ICommand
    {
        string Type { get; }
    }
}
