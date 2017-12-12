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

namespace Lab6
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

        private Ellipse _selectedMarker = null;
        public Ellipse SelectedMarker
        {
            get { return _selectedMarker; }
            set
            {
                if (value == null)
                {
                    if (_selectedMarker != null)
                        _selectedMarker.Stroke = (_selectedMarker.DataContext as Marker).Color;
                }
                else
                    value.Stroke = Brushes.Red;
                _selectedMarker = value;
            }
        }
        private bool _ellipseMoving = false;
    }
    public enum EditModeType
    {
        View,
        Rotate,
        Resize,
        Manipulate
    }
}
