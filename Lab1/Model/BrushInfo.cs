using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab1.Model
{
    public class BrushInfo
    {
        public string Caption { get; private set; }
        public Brush Color { get; private set; }
        public BrushInfo(string c, Brush color)
        {
            Caption = c;
            Color = color;
        }
    }
}
