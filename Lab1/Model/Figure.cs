using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Lab1.Model
{
    public class Figure : DependencyObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int DefaultX { get; set; }
        public int DefaultY { get; set; }
        public Size DefaultSize { get; set; }
        public Rectangle Rect { get; set; }
        public Ellipse Ellipse { get; set; }
        //public event PropertyChangedEventHandler PropertyChanged;
    }
}
