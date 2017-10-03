using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab1.ViewModel.Commands
{
    public class AddFigureCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var vm = parameter as MainViewModel;

            int x1 = vm.X1, y1 = vm.Y1, x2 = vm.X2, y2 = vm.Y2;

            if (y2 >= y1)
            {
                System.Windows.Forms.MessageBox.Show("Ooops, looks like there's no way to do this.");
                return;
            }

            double angle = Math.Atan2(y1 - y2, x1 - x2) / Math.PI * 180;
            //angle = (angle < 0) ? angle + 360 : angle;
            Console.WriteLine(angle - 90);
            var dist = Math.Round(Math.Sqrt(Math.Pow(y1 - y2, 2) + Math.Pow(x1 - x2, 2)), 3);
            Console.WriteLine(dist);
            var axleOffset = (dist * (Math.Sin((angle - 90) / 57.2957795)));//, 0);
            Console.WriteLine(axleOffset);

            var figure = new Model.Figure()
            {
                X = x1,
                Y = y1,
                DefaultX = x1,
                DefaultY = y1,
                Rect = new Rectangle()
                {
                    Height = dist,
                    Width = dist,
                    Stroke = vm.SelectedRectColor.Color,
                    StrokeThickness = 2
                },
                Ellipse = new Ellipse()
                {
                    Width = dist,
                    Height = dist,
                    Stroke = vm.SelectedEllipseColor.Color,
                    StrokeThickness = 2
                },
                DefaultSize = new Size(dist, dist)
            };
            if (axleOffset > 0)
                figure.Y += (int)axleOffset;
            if (axleOffset < 0)
                figure.X += (int)axleOffset;

            Console.WriteLine(figure.X + " ; " + figure.Y);

            figure.Rect.LayoutTransform = new RotateTransform(0 - angle - 90, 0, 0);
            figure.Ellipse.LayoutTransform = new RotateTransform(0 - angle - 90, 0, 0);

            //if (vm.Fi)
            vm.Figures.Add(figure);
        }
    }
}
