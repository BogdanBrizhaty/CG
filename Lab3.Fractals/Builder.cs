using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab3.Fractals
{
    public class Builder
    {
        static double _from;
        static double _pLen;
        static int _cX;
        static int _cY;
        static Brush _color;
        static bool _first;
        static List<Line> _fract;
        public static IList<Line> Build(int iterations, int width, int cX, int cY, Brush color)
        {
            _from = cX - width / 2;
            _cX = cX;
            _color = color;
            _cY = cY;
            _fract = new List<Line>();
            _pLen = (iterations == 0) ? width : width / Math.Pow(3, iterations);
            _first = true;
            Build(iterations, 0);
            return _fract;
        }
        private static void Build(int iteration, int angle)
        {
            Line l = null;

            if (iteration == 0)
            {
                if (_first)
                {
                    _first = false;
                    l = new Line() { Stroke = _color, StrokeThickness = 2, X1 = _from, X2 = _from + _pLen, Y1 = _cY, Y2 = _cY };
                    _fract.Add(l);
                }
                else
                {
                    var lastLine = _fract.Last();
                    var transform = new RotateTransform(angle, lastLine.X2, lastLine.Y2);
                    var line = new System.Windows.Shapes.Line()
                    {
                        X1 = lastLine.X2,
                        Y1 = lastLine.Y2,
                        X2 = lastLine.X2 + _pLen,
                        Y2 = lastLine.Y2,
                        Stroke = _color,
                        StrokeThickness = 2
                    };

                    var newPoint = transform.Transform(new Point(line.X2, line.Y2));
                    line.X2 = newPoint.X;
                    line.Y2 = newPoint.Y;
                    l = line;
                    _fract.Add(line);
                }
                return;
            }

            Build(iteration - 1, angle + 0);
            Build(iteration - 1, angle + -60);
            Build(iteration - 1, angle + 60);
            Build(iteration - 1, angle + 0);

        }
    }
}
