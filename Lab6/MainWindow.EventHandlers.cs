using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab6
{
    public partial class MainWindow
    {
        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (AppDataContext.Instance.AppMode == EditModeType.Manipulate)
            {
                _ellipseMoving = true;
                if ((sender as Ellipse) == SelectedMarker)
                    SelectedMarker = null;
                else
                    SelectedMarker = (sender as Ellipse);

                return;
            }
            if (SelectedMarker != null)
                SelectedMarker.Stroke = Brushes.LightGray;
            SelectedMarker = (sender as Ellipse);
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (AppDataContext.Instance.AppMode == EditModeType.Manipulate)
            {
                _ellipseMoving = false;
                SelectedMarker = null;
            }
        }
        int i = 0;
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
        private void moveWholeTriangle(Marker movable, Marker oldCenter, Marker newCenter)
        {
            var dx = oldCenter.X - movable.X;
            var dy = oldCenter.Y - movable.Y;

            movable.X = newCenter.X - dx;
            movable.Y = newCenter.Y - dy;
        }
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (AppDataContext.Instance.AppMode == EditModeType.Manipulate)
                if (SelectedMarker != null && _ellipseMoving)
                {
                    var pos = e.GetPosition(layout);
                    var marker = (SelectedMarker.DataContext as Marker);

                    var oldMarker = marker.Clone() as Marker;

                    marker.X = pos.X - marker.Radius;
                    marker.Y = pos.Y - marker.Radius;

                    if (SelectedMarker.Name == String.Empty)
                    {
                        AppDataContext.Instance.RecalculateCenter();
                        return;
                    }
                    moveWholeTriangle(AppDataContext.Instance.Side1.StartAt, oldMarker, marker);
                    moveWholeTriangle(AppDataContext.Instance.Side2.StartAt, oldMarker, marker);
                    moveWholeTriangle(AppDataContext.Instance.Side3.StartAt, oldMarker, marker);

                }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (AppDataContext.Instance.AppMode == EditModeType.Manipulate)
            {
                _ellipseMoving = false;
                SelectedMarker = null;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AppDataContext.Instance.AppMode = (AppDataContext.Instance.AppMode == EditModeType.View) ?
                 EditModeType.Manipulate : EditModeType.View;

            paramsPanel.Visibility = (paramsPanel.Visibility == Visibility.Collapsed) ?
                Visibility.Visible : Visibility.Collapsed;

            if (AppDataContext.Instance.AppMode == EditModeType.Manipulate)
            {
                foreach (var item in layout.Children)
                    if (item is Ellipse)
                        (item as Ellipse).Stroke = ((item as Ellipse).DataContext as Marker).Color;
            }
            else
                foreach (var item in layout.Children)
                    if (item is Ellipse)
                        (item as Ellipse).Stroke = Brushes.LightGray;
            //if ((byte)AppDataContext.Instance.AppMode == 3)
            //    AppDataContext.Instance.AppMode = 0;
            //else
            //    AppDataContext.Instance.AppMode++;
        }
        private void SetAngle(Marker m, double radian)
        {
            Marker sp = SelectedMarker.DataContext as Marker;
            var x = m.X - sp.X;
            var y = m.Y - sp.Y;
            m.X = sp.X + x *
                (Math.Cos(radian)) + y * (Math.Sin(radian));
            m.Y = sp.Y + y *
                (Math.Cos(radian)) - x * (Math.Sin(radian));
        }
        private void applyAngleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMarker == null)
            {
                System.Windows.Forms.MessageBox.Show("Select rotation point");
                return;
            }

            var angle = DegreeToRadian(AppDataContext.Instance.Angle);
            SetAngle(AppDataContext.Instance.Side1.StartAt, angle);
            SetAngle(AppDataContext.Instance.Side2.StartAt, angle);
            SetAngle(AppDataContext.Instance.Side3.StartAt, angle);
            AppDataContext.Instance.RecalculateCenter();

        }

        private void setNewMarkerPos(Marker m, double oldScale, double newScale)
        {
            var newCenter = AppDataContext.Instance.TriangleCenter;
            var @new = new Marker(
                newCenter.RenderedX - ((newCenter.RenderedX - m.RenderedX) / oldScale) * newScale,
                newCenter.RenderedY - ((newCenter.RenderedY - m.RenderedY) / oldScale) * newScale);
            m.X = @new.X;
            m.Y = @new.Y;
        }
        private void decScaleBtn_Click(object sender, RoutedEventArgs e)
        {
            var oldScale = (double)AppDataContext.Instance.Scale;
            AppDataContext.Instance.Scale -= AppDataContext.ScaleStep;
            var newScale = oldScale - (double)AppDataContext.ScaleStep;

            setNewMarkerPos(AppDataContext.Instance.Side1.StartAt, oldScale, newScale);
            setNewMarkerPos(AppDataContext.Instance.Side2.StartAt, oldScale, newScale);
            setNewMarkerPos(AppDataContext.Instance.Side3.StartAt, oldScale, newScale);
        }

        private void incScaleBtn_Click(object sender, RoutedEventArgs e)
        {
            var oldScale = (double)AppDataContext.Instance.Scale;
            AppDataContext.Instance.Scale += AppDataContext.ScaleStep;
            var newScale = oldScale + (double)AppDataContext.ScaleStep;

            setNewMarkerPos(AppDataContext.Instance.Side1.StartAt, oldScale, newScale);
            setNewMarkerPos(AppDataContext.Instance.Side2.StartAt, oldScale, newScale);
            setNewMarkerPos(AppDataContext.Instance.Side3.StartAt, oldScale, newScale);
        }
    }
}
