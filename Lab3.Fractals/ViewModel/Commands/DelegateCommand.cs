using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab3.Fractals.ViewModel.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> _action;
        private INotifyPropertyChanged _vm;
        public DelegateCommand(Action<object> action)
        {
            _action = action;
        }
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            _action.Invoke(parameter);
        }
    }
}
