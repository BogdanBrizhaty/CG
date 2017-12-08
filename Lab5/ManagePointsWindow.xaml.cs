using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for ManagePointsWindow.xaml
    /// </summary>
    public partial class ManagePointsWindow : Window
    {
        private ObservableCollection<Marker> _markers;
        public ObservableCollection<Marker> Markers
        {
            get { return _markers; }
        }

        private Ellipse _selectedMarker = null;
        public Ellipse SelectedMarker
        {
            get
            {
                return _selectedMarker;
            }
            set
            {
                if (value == null)
                {
                    if (_selectedMarker != null)
                        _selectedMarker.Stroke = Brushes.Blue;
                }
                else
                    value.Stroke = Brushes.Red;
                _selectedMarker = value;
            }
        }
        public ManagePointsWindow()
        {
            _markers = new ObservableCollection<Marker>(new ObservableCollection<Marker>(MainWindowDataContext.
                Instance.
                Figures.
                Where(f => f.GetType() == typeof(Marker)).
                Cast<Marker>()));
            InitializeComponent();
        }
        private bool _ellipseMoving = false;
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                // remove marker here
                var removable = ((sender as Ellipse).DataContext as Marker);
                Markers.Remove(removable);
                MainWindowDataContext.Instance.Figures.Remove(removable);
                return;
            }
            _ellipseMoving = true;
            if ((sender as Ellipse) == SelectedMarker)
                SelectedMarker = null;
            else
                SelectedMarker = (sender as Ellipse);
        }
        

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Canvas)
            {
                _ellipseMoving = false;
                SelectedMarker = null;
                var pos = e.GetPosition(sender as Canvas);
                var newMarker = new Marker(pos.X, pos.Y);
                MainWindowDataContext.Instance.Figures.Add(newMarker);
                Markers.Add(newMarker);
            }
        }
        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _ellipseMoving = false;
            SelectedMarker = null;
        }

        private void cnvs_MouseMove(object sender, MouseEventArgs e)
        {
                if (SelectedMarker != null && _ellipseMoving)
                {
                    var marker = (SelectedMarker.DataContext as Marker);
                    var pos = e.GetPosition(cnvsSrc);
                    marker.X = pos.X - 5 - marker.Radius;
                    marker.Y = pos.Y - 5 - marker.Radius;
                }
        }

        private void cnvs_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _ellipseMoving = false;
            SelectedMarker = null;
        }
    }
}
