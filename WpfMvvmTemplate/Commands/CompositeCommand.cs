using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WpfMvvmTemplate.Commands
{
    public class CompositeCommand : ICommand
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public event EventHandler CanExecuteChanged
        {
            add
            {
                foreach(var command in _commands)
                    command.CanExecuteChanged += value;
            }
            remove
            {
                foreach(var command in _commands)
                    command.CanExecuteChanged -= value;
            }
        }

        public void RegisterCommand(ICommand command)
        {
            if(!_commands.Contains(command))
                _commands.Add(command);
        }

        public void UnregisterCommand(ICommand command)
        {
            if(_commands.Contains(command))
                _commands.Remove(command);
        }

        public bool CanExecute(object parameter)
        {
            foreach(var command in _commands)
            {
                if(!command.CanExecute(parameter))
                    return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            foreach(var command in _commands)
            {
                command.Execute(parameter);
            }
        }
    }
}
