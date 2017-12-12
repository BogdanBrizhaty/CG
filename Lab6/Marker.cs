using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Lab6
{
    public class Marker : INotifyPropertyChanged, ICloneable
    {
        private double _x;
        private double _y;
        private double _diameter = 8;

        private Brush _color;

        public double Diameter
        {
            get { return _diameter; }
            set
            {
                var oldDiameter = _diameter;
                if (value >= 0.5d)
                {
                    _diameter = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Diameter"));
                    X = oldDiameter / 2 - _diameter / 2;
                }
            }
        }
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("X"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RenderedX"));
            }
        }

        public Brush Color { get; set; }
        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Y"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RenderedY"));
            }
        }
        public double RenderedX
        {
            get { return _x + Radius; }
        }
        public double RenderedY
        {
            get { return _y + Radius; }
        }

        public double Radius
        {
            get { return _diameter / 2; }
        }
        public static Marker FromPoint(Point point)
        {
            return new Marker(point.X, point.Y);
        }
        public static Marker FromPoint(Point point, double diameter)
        {
            return new Marker(point.X, point.Y, diameter);
        }
        public Point ToPoint()
        {
            return new Point(X + Radius, Y + Radius);
        }
        public Marker(double x, double y) : this(x, y, 8)
        {
            Color = Brushes.Green;
            X = x - Diameter / 2;
            Y = y - Diameter / 2;
        }

        public Marker(double x, double y, double diameter) 
        {
            Diameter = diameter;
        }

        public override string ToString()
        {
            return String.Format("{0,10} ; {1,10}", X, Y);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
