using CQRS_Write_Domain.Commands;
using CQRS_Write_Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CQRS_Write_Infrastructure.Commands
{
    public class CommandBus : ICommandBus
    {
        private static Dictionary<Type, List<Action<ICommand>>> commandHandlerDictionary = new Dictionary<Type, List<Action<ICommand>>>();
        private static Dictionary<Type, List<Action<IEvent>>> eventHandlerDictionary = new Dictionary<Type, List<Action<IEvent>>>();


        public void Publish<T>(T @event) where T : IEvent
        {
            List<Action<IEvent>> eventActions;
            if (eventHandlerDictionary.TryGetValue(@event.GetType(), out eventActions))
                foreach (var eventhandleMethod in eventActions)
                {
                    eventhandleMethod(@event);
                }
            else
            {
                throw new InvalidOperationException($"Evento não foi encontrado {@event}");
            }
        }

        public void Send<T>(T command) where T : ICommand
        {
            List<Action<ICommand>> commandActions;
            if (commandHandlerDictionary.TryGetValue(command.GetType(), out commandActions))
                foreach (var commandthandleMethod in commandActions)
                {
                    commandthandleMethod(command);
                }
            else
            {
                throw new InvalidOperationException($"Comando não foi encontrado {command}");
            }
        }

        public void RegisterCommandHandlers(ICommandHandler commandHandler)
        {
            var commandhandlerMethod = commandHandler.GetType().GetMethods()
                  .Where(m => m.GetParameters()
                  .Any(p => p.ParameterType.GetInterfaces().Contains(typeof(ICommand))));

            foreach (var method in commandhandlerMethod)
            {
                ParameterInfo commandParameterInfo = method.GetParameters()
                                   .Where(p => p.ParameterType.GetInterfaces().Contains(typeof(ICommand))).FirstOrDefault();
                if (commandParameterInfo == null) continue;

                Type commandParameterType = commandParameterInfo.ParameterType;

                List<Action<ICommand>> commandActions;
                if (!commandHandlerDictionary.TryGetValue(commandParameterType, out commandActions))
                {
                    commandActions = new List<Action<ICommand>>();
                    commandHandlerDictionary.Add(commandParameterType, commandActions);
                }

                commandActions.Add(x => method.Invoke(commandHandler, new object[] { x }));
            }

        }

        public void RegisterEventHandlers(IEventHandler eventHandlers)
        {
            var commandhandlerMethods = eventHandlers.GetType().GetMethods()
                           .Where(m => m.GetParameters()
                           .Any(p => p.ParameterType.GetInterfaces().Contains(typeof(IEvent))));

            foreach (var method in commandhandlerMethods)
            {
                ParameterInfo commandParameterInfo = method.GetParameters()
                    .Where(p => p.ParameterType.GetInterfaces().Contains(typeof(IEvent))).FirstOrDefault();

                if (commandParameterInfo == null) continue;

                Type commandParameterType = commandParameterInfo.ParameterType;
                List<Action<IEvent>> eventActions;

                if (!eventHandlerDictionary.TryGetValue(commandParameterType, out eventActions))
                {
                    eventActions = new List<Action<IEvent>>();
                    eventHandlerDictionary.Add(commandParameterType, eventActions);
                }

                eventActions.Add(x => method.Invoke(eventHandlers, new object[] { x }));
            }
        }


    }
}
