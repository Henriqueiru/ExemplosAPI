using CQRS_Write_Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Write_Domain.Commands
{
    public interface ICommandBus : ICommandSender, IEventPublisher
    {
    }
}
