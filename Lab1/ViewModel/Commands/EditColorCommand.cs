using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab1.ViewModel.Commands
{
    class EditColorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var vm = parameter as MainViewModel;
            if (vm != null && vm.SelectedFigure != null)
            {
                vm.SelectedFigure.Rect.Stroke = vm.SelectedRectColor.Color;
                vm.SelectedFigure.Ellipse.Stroke = vm.SelectedEllipseColor.Color;
                vm.SelectedFigure.Rect.Fill = vm.SelectedRectColor.Color;
                vm.SelectedFigure.Ellipse.Fill = vm.SelectedEllipseColor.Color;
            }
        }
    }
}
