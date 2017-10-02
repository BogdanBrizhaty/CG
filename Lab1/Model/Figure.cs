using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Lab1.Model
{
    public class Figure : BaseNotifyingModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Rectangle Rect { get; set; }
        public Ellipse Ellipse { get; set; }
    }
}
