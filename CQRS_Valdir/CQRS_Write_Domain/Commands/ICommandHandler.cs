using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.Commands
{
    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        void Handle(T command);
    }

    public interface ICommandHandler { }
}
