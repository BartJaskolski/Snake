using System;
using System.Windows.Input;

namespace SnakeMVVM.Commands
{
    public class PlayerControlCommnad : ICommand
    {
        private Action<object> ExecuteMethod;
        private Func<object, bool> CanExecuteMehtd;

        public PlayerControlCommnad(Action<object> executeMethod, Func<object, bool> canExecuteMehtd)
        {
            ExecuteMethod = executeMethod;
            CanExecuteMehtd = canExecuteMehtd;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (CanExecuteMehtd != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (CanExecuteMehtd != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
    }
}
