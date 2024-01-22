using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ADO.NET_HOME_WORK_1
{
    public class RealCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove 
            { 
                CommandManager.RequerySuggested -= value;
            }
        }

        public readonly Predicate<object?>? predicate;
        public readonly Action<object?>? action;

        public RealCommand(Action<object?>? action, Predicate<object?>? predicate = null)
        {
            this.predicate = predicate;
            this.action = action;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter == null) { return true; }

            return predicate!.Invoke(parameter);
        }

        public void Execute(object? parameter)
        {
            action?.Invoke(parameter);
        }
    }
}
