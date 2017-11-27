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

namespace Lab3.Fractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //@is.Items.Add(new Line());
            //var rect = new Rectangle() { Height = 50, Width = 50, Stroke = Brushes.Black,StrokeThickness = 2 };

            //cnvs.Children.Add(rect);
            //ZoomableCanvas.SetTop(rect, 50);
            //ZoomableCanvas.SetLeft(rect, 50);
        }
    }
}
