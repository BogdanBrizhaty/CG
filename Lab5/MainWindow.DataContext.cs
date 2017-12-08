using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab5
{
    public class MainWindowDataContext : INotifyPropertyChanged
    {
        private ObservableCollection<object> _figures = new ObservableCollection<object>();
        private PointCollection _buildPoints = null;
        private int _pointsPerCurve = 0;

        private ObservableCollection<Marker> _markers = new ObservableCollection<Marker>();
        public ObservableCollection<Marker> Markers
        {
            get { return _markers; }
        }

        private static MainWindowDataContext _instance = null;
        private BezierCurve _bezCurv = null;
        
        public MainWindowDataContext()
        {
            _instance = this;
            PointsPerCurve = 20;
            // set polyline and binding
            var poly = new Polyline() { StrokeThickness = 2, Stroke = Brushes.Green };
            Binding myBinding = new Binding("BuildPoints");
            myBinding.Source = this;
            poly.SetBinding(Polyline.PointsProperty, myBinding);
            Figures.Add(poly);
            var p1 = new Point(100, 200);
            var p2 = new Point(150, 250);
            var p3 = new Point(200, 150);
            var p4 = new Point(250, 200);
            _bezCurv = new BezierCurve(new List<Point>() { p1, p2, p3, p4/*, new Point(300, 300)*/ }, this._pointsPerCurve);
            BuildPoints = new PointCollection(new ObservableCollection<Point>(_bezCurv.DrawingPoints));
            Figures.Add(Marker.FromPoint(p1));
            Figures.Add(Marker.FromPoint(p2));
            Figures.Add(Marker.FromPoint(p3));
            Figures.Add(Marker.FromPoint(p4));
            Figures.CollectionChanged += Figures_CollectionChanged;

            _markers = new ObservableCollection<Marker>(
                Figures.Where(f => f is Marker).Cast<Marker>()
                );
        }

        public void Rebuild(Point old, Point @new)
        {
            var rIndex = _bezCurv.DataPoints.FindIndex(p => p.X == old.X && p.Y == old.Y);
            Console.WriteLine(rIndex);
            _bezCurv[rIndex] = @new;
            _bezCurv.Invalidate();
            BuildPoints = new PointCollection(_bezCurv.DrawingPoints);
        }

        private void Figures_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Markers.Add(e.NewItems[0] as Marker);
                _bezCurv.DataPoints.Add((e.NewItems[0] as Marker).ToPoint());
                _bezCurv.Invalidate();
                BuildPoints = new PointCollection(_bezCurv.DrawingPoints);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var removable = e.OldItems[0] as Marker;
                var rIndex = _bezCurv.DataPoints.FindIndex(p => p.X == removable.ToPoint().X && p.Y == removable.ToPoint().Y);
                _bezCurv.DataPoints.RemoveAt(rIndex);
                _bezCurv.Invalidate();
                BuildPoints = new PointCollection(_bezCurv.DrawingPoints); 
                RaisePropertyChanged("BuildPoints");
                Markers.Remove(removable);
            }
        }

        public static MainWindowDataContext Instance
        {
            get { return _instance; }
        }
        public int PointsPerCurve
        {
            get { return _pointsPerCurve; }
            set
            {
                if (value >= 5)
                {
                    _pointsPerCurve = value;
                    if (_bezCurv != null)
                    {
                        _bezCurv.PointsPerCurve = value;
                        _bezCurv.Invalidate();
                        BuildPoints = new PointCollection(_bezCurv.DrawingPoints);
                    }
                    RaisePropertyChanged();
                }
            }
        }
        public PointCollection BuildPoints
        {
            get { return _buildPoints; }
            set
            {
                _buildPoints = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<object> Figures
        {
            get { return _figures; }
            set { _figures = value; RaisePropertyChanged(); }
        }

        #region INotifyPropertyChanged interface implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
