using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace dotNet5781_03B_7073_1160
{
    public static class Utils
    {
        public class RelayCommand : ICommand
        {
            private Action<object> execute;
            private Func<object, bool> canExecute;

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return canExecute == null || this.canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                execute(parameter);
            }
        }


        public static bool IsDigitsOnly(string str)//Checks if the license number is just digits
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
