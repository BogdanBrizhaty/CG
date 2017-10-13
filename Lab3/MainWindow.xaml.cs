using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _modifier = Math.Sqrt(2);
            Console.WriteLine(_modifier);
        }
        readonly double _modifier;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var line1 = new Line() { Stroke = Brushes.Black, X1 = 10, Y1 = 50, X2 = 50, Y2 = 50, RenderTransform = new RotateTransform(-45, 10, 50) };
            //view.Children.Add(line1);
            //var point1 = new Ellipse() { Stroke = Brushes.Blue, StrokeThickness = 5 };
            //Canvas.SetLeft(point1, 7.5);
            //Canvas.SetTop(point1, 47.5);
            //view.Children.Add(point1);

            //Console.WriteLine(line1.X1 / _modifier);
            //Console.WriteLine(line1.Y1 / _modifier);
            //var line2 = new Line()
            //{
            //    Stroke = Brushes.Black,
            //    X1 = line1.X1 + 40 / _modifier,
            //    Y1 = line1.Y1 - 40 / _modifier,
            //    X2 = line1.X1 + 40 / _modifier + 40,
            //    Y2 = line1.Y1 - 40 / _modifier,
            //    RenderTransform = new RotateTransform(45, line1.X1 + 40 / _modifier, line1.Y1 - 40 / _modifier)
            //};
            //view.Children.Add(line2);
            //var point2 = new Ellipse() { Stroke = Brushes.Green, StrokeThickness = 5 };
            //Canvas.SetLeft(point2, line1.X1 + 40 / _modifier - point2.StrokeThickness / 2);
            //Canvas.SetTop(point2, line1.Y1 - 40 / _modifier - point2.StrokeThickness / 2);
            //view.Children.Add(point2);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            lin1.RenderTransform = new ScaleTransform(4, 1, 50, 50);
            line2.RenderTransform = new TransformGroup() { Children = new TransformCollection() { new ScaleTransform(1.2, 1, 150, 50), new RotateTransform(45, 150, 50) } };
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            lin1.RenderTransform = new ScaleTransform(0.8, 1, 50, 50);
            line2.RenderTransform = new TransformGroup() { Children = new TransformCollection() { new ScaleTransform(0.8, 1, 150, 50), new RotateTransform(45, 150, 50) } };

        }
    }
}
