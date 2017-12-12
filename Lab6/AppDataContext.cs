using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab6
{
    public class AppDataContext : INotifyPropertyChanged
    {
        private const decimal MinScale = 0.1m;
        private const decimal MaxScale = 2.000m;
        public const decimal ScaleStep = 0.1m;

        private Marker _p1 = null;
        private Marker _p2 = null;
        private Marker _p3 = null;
        private Marker _triangleCenter = null;

        private decimal _scale = 1.0M;
        private int _angle = 1;

        private Side _s1 = null;
        private Side _s2 = null;
        private Side _s3 = null;

        private EditModeType _appMode;

        private static AppDataContext _instance = null;

        public event PropertyChangedEventHandler PropertyChanged;

        public Marker TriangleCenter
        {
            get { return _triangleCenter; }
        }

        public int Angle
        {
            get { return _angle; }
            set { _angle = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Angle")); }
        }

        public static AppDataContext Instance
        {
            get { return _instance; }
        }

        public decimal Scale
        {
            get { return _scale; }
            set
            {
                if (value >= MinScale && value <= MaxScale)
                {
                    _scale = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Scale")); 
                }
            }
        }

        public EditModeType AppMode
        {
            get { return _appMode; }
            set { _appMode = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AppMode")); }
        }

        public Side Side1
        {
            get { return _s1; }
        }
        public Side Side2
        {
            get { return _s2; }
        }
        public Side Side3
        {
            get { return _s3; }
        }

        public void RecalculateCenter()
        {
            TriangleCenter.X = (_p1.X + _p2.X + _p3.X) / 3;
            TriangleCenter.Y = (_p1.Y + _p2.Y + _p3.Y) / 3;
        }

        public AppDataContext()
        {
            AppMode = EditModeType.Manipulate;
            _p1 = new Marker(210, 210);
            _p2 = new Marker(300, 300);
            _p3 = new Marker(210, 300);
            _triangleCenter = new Marker(210, 300) { Color = Brushes.Blue };
            RecalculateCenter();
            _s1 = new Side(_p1, _p2);
            _s2 = new Side(_p2, _p3);
            _s3 = new Side(_p3, _p1);
            _instance = this;


        }

    }
}
