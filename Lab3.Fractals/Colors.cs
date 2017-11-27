using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab3.Fractals
{
    public class BrushInfo
    {
        public string Name { get; protected set; }
        public Brush Brush { get; protected set; }
        public BrushInfo(string name, Brush brush)
        {
            Name = name;
            Brush = brush;
        }
    }
    public static class Colors
    {
        private static List<BrushInfo> _colors = new List<BrushInfo>();
        static Colors()
        {
            var res = (typeof(Brushes)).GetProperties();
            foreach (var i in res)
            {
                _colors.Add(new BrushInfo(i.Name,
                    (Brush)(typeof(Brushes)).GetProperty(i.Name).GetValue(null)));
            }
        }
        public static List<BrushInfo> ColorList
        {
            get { return _colors; }
        }
    }
}
