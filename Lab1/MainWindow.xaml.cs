using Lab1.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var nl = Environment.NewLine;
            var text = "";
            text += "Created at Lviv Politech State University" + nl;
            text += "on date 11 october 2017" + nl;
            text += "-------------------------" + nl;
            text += "Description: Лабораторна робота №1" + nl;
            text += "Програма дозволяє розміщувати на площині квадрати за заданими координатами та описані кола навколо них" + nl;
            text += "-------------------------" + nl;
            text += "Author: Bogdan Brizhaty" + nl;
            text += "All rights reserved (c)";
            System.Windows.Forms.MessageBox.Show(text, "About");
        }


    }
}
