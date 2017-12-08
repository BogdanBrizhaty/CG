using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab5
{
    /// <summary>
    /// Кривоая безье
    /// </summary>
    public class BezierCurve
    {
        /// <summary>
        /// Количество точек для отрисовки.
        /// </summary>
        private int _n = 10;

        public int PointsPerCurve
        {
            get { return _n; }
            set { _n = value; }
        }

        /// <summary>
        /// Опорные точки.
        /// </summary>
        private List<Point> dataPoints;

        /// <summary>
        /// Создание кривой безье
        /// </summary>
        /// <param name="points">Опроные точки</param>
        public BezierCurve(Point[] points)
        {
            dataPoints = points.ToList();
            Invalidate();
        }
        public BezierCurve(IEnumerable<object> markers)
        {
            var lpoints = new List<Point>();
            foreach (var marker in markers)
                lpoints.Add(new Point(((Marker)marker).X, ((Marker)marker).Y));
            dataPoints = lpoints;
            Invalidate();
        }
        public BezierCurve(IEnumerable<Point> markers, int pCount)
        {
            PointsPerCurve = pCount;
            Console.WriteLine("ppc: " + pCount);
            DataPoints = markers.ToList();
            //var lpoints = new List<Point>();
            //foreach (var marker in markers)
            //    lpoints.Add(new Point(((Marker)marker).X, ((Marker)marker).Y));
            //dataPoints = lpoints;
            Invalidate();
        }

        /// <summary>
        /// Точки для отрисовки
        /// </summary>
        public Point[] DrawingPoints { get; private set; }

        /// <summary>
        /// Опорные точки
        /// </summary>
        public List<Point> DataPoints
        {
            get { return dataPoints; }
            set
            {
                dataPoints = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Опорная точка
        /// </summary>
        /// <param name="i">Индекс опорной точки</param>
        public Point this[int i]
        {
            get { return dataPoints[i]; }
            set
            {
                dataPoints[i] = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Обновить точки для отрисовки.
        /// </summary>
        public void Invalidate()
        {
            DrawingPoints = new Point[_n + 1];
            double dt = 1d / _n;
            double t = 0d;
            for (int i = 0; i <= _n; i++)
            {
                DrawingPoints[i] = B(t);
                t += dt;
            }
        }
        int Factorial(int numb)
        {
            int res = 1;
            for (int i = numb; i > 1; i--)
                res *= i;
            return res;
        }
        /// <summary>
        /// Функция кривой
        /// </summary>
        /// <param name="t">Параметр. Может изменяться от 0 до 1</param>
        /// <returns>Точка кирвой</returns>
        private Point B(double t)
        {
            var pCount = DataPoints.Count - 1;
            var xPoint = 0d;
            var yPoint = 0d;
                for (int i = 0; i < DataPoints.Count; i++)
                {
                    xPoint += this[i].X * (Factorial(pCount) / (Factorial(i) * Factorial(pCount - i))) * Math.Pow(t, i) * Math.Pow((1 - t), pCount - i);
                    yPoint += this[i].Y * (Factorial(pCount) / (Factorial(i) * Factorial(pCount - i))) * Math.Pow(t, i) * Math.Pow((1 - t), pCount - i);
                }
            return new Point(xPoint, yPoint);
        }
    }
}
