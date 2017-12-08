using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Lab5
{
    public partial class MainWindow
    {
        // open window for manage points
        private void manageDataPointsButton_Click(object sender, RoutedEventArgs e)
        {
            (new ManagePointsWindow()).ShowDialog();
        }

        private void incPointsPerCurve_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDataContext.Instance.PointsPerCurve++;
        }

        private void decPointsPerCurve_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDataContext.Instance.PointsPerCurve--;
        }
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                // remove marker here
                var removable = ((sender as Ellipse).DataContext as Marker);
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
                if (e.ChangedButton == MouseButton.Left)
                {
                    var pos = e.GetPosition(sender as Canvas);
                    MainWindowDataContext.Instance.Figures.Add(new Marker(pos.X, pos.Y));
                }
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
                var pos = e.GetPosition(cnvsSrc);
                var marker = (SelectedMarker.DataContext as Marker);
                var old = marker.ToPoint();
                marker.X = pos.X - 5 - marker.Radius;
                marker.Y = pos.Y - 5 - marker.Radius;
                MainWindowDataContext.Instance.Rebuild(old, marker.ToPoint());
            }
        }

        private void cnvs_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _ellipseMoving = false;
            SelectedMarker = null;
        }
    }
}
