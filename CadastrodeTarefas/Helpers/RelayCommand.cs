using System;
using System.Windows.Input;

namespace TodoApp.Helpers
{
   public class RelayComand : ICommand
    {
        private readonly Action<object> _execute;
        
        public RelayComand (Action<object> execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
              
}

