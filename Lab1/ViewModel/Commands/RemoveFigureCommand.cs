using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab1.ViewModel.Commands
{
    class RemoveFigureCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var vm = parameter as MainViewModel;
            if (vm.SelectedFigure != null)
                vm.Figures.Remove(vm.SelectedFigure);
        }
    }
}
