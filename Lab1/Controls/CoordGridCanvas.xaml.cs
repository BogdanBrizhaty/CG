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

namespace Lab1.Controls
{
    /// <summary>
    /// Interaction logic for CoordGridCanvas.xaml
    /// </summary>
    public partial class CoordGridCanvas : UserControl, ITransformable, IScalable
    {
        //private int _scale = new Binding
        public CoordGridCanvas()
        {
            InitializeComponent();
        }
        public void Centre()
        {
            throw new NotImplementedException();
        }

        public void Scale(int scale)
        {
            throw new NotImplementedException();
        }

        public void Transform()
        {
            throw new NotImplementedException();
        }
    }
}
