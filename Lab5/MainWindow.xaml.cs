using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Lab5
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
                        _selectedMarker.Stroke = Brushes.Blue;
                }
                else
                {
                    value.Stroke = Brushes.Red;
                    lst.SelectedItem = value.DataContext;
                }
                    _selectedMarker = value;
            }
        }
        private bool _ellipseMoving = false;

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!_ellipseMoving)
                (sender as Ellipse).Stroke = Brushes.Yellow;
        }

        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!_ellipseMoving)
                (sender as Ellipse).Stroke = Brushes.Blue;
        }
    }
}
