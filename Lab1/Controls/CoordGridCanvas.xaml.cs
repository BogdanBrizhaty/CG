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

namespace Lab1.Controls
{
    /// <summary>
    /// Interaction logic for CoordGridCanvas.xaml
    /// </summary>
    public partial class CoordGridCanvas : UserControl, ITransformable, IScalable, INotifyPropertyChanged
    {
        private Rect _canvasViewport = new Rect(new Point(-12, -12), new Size(25, 25));
        private Rect _brushGeometry = new Rect(new Point(0, 0), new Size(25, 25));

        public Rect CanvasViewPort
        {
            get { return _canvasViewport; }
            set
            {
                _canvasViewport = value;
                OnPropertyChanged("CanvasViewPort");
            }
        }

        public Rect BrushGeometry
        {
            get { return _brushGeometry; }
            set
            {
                _brushGeometry = value;
                OnPropertyChanged("BrushGeometry");
            }
        }


        //private int _scale = new Binding
        public CoordGridCanvas()
        {
            InitializeComponent();

            Centre(new Size(canvas.Height, canvas.Width));

            int newSize = (int)(CellSize * CurrentScale);
            CanvasViewPort = new Rect(
                new Point(-1 * (newSize / 2), -1 * (newSize / 2)),
                new Size(newSize, newSize));
            BrushGeometry = new Rect(new Point(0, 0), new Size(newSize, newSize));

        }
        public void Centre(Size e)
        {
            var cellSize = (int)(CellSize * CurrentScale);

            var cWidth = (cellSize * (int)(e.Width / cellSize)) / 2;
            var cHeight = (cellSize * (int)(e.Height / cellSize)) / 2;

            //Console.WriteLine(cWidth + "    " + cHeight);
            if (cWidth % cellSize != 0)
                CoordCentre = new Point(cWidth, CoordCentre.Y);
            else 
                CoordCentre = new Point(cWidth - (int)(cellSize / 2), CoordCentre.Y);
            if (cHeight % cellSize != 0)
                CoordCentre = new Point(CoordCentre.X, cHeight);
            else
                CoordCentre = new Point(CoordCentre.X, cHeight - (int)(cellSize / 2));
            if (cWidth % cellSize != 0 && cHeight % cellSize != 0)
                CoordCentre = new Point(cWidth, cHeight);
        }
        //public Point CoordCentre { get; set; }
        public Point CoordCentre
        {
            get { return (Point)this.GetValue(CoordCentreProperty); }
            set
            {
                //Console.WriteLine(CoordCentre.ToString());
                var oldValue = CoordCentre;
                this.SetValue(CoordCentreProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(CoordCentreProperty, oldValue, value));
            }
        }
        public static readonly DependencyProperty CoordCentreProperty = DependencyProperty.Register(
          "CoordCentre", typeof(Point), typeof(CoordGridCanvas),
          new PropertyMetadata(new Point(0, 0)));

        public decimal CurrentScale
        {
            get { return (decimal)this.GetValue(CurrentScaleProperty); }
            set { this.SetValue(CurrentScaleProperty, value); }
        }
        public static readonly DependencyProperty CurrentScaleProperty = DependencyProperty.Register(
          "CurrentScale", typeof(decimal), typeof(CoordGridCanvas),
          new PropertyMetadata(1.00M, new PropertyChangedCallback(OnScaleChanged)));

        public int CellSize
        {
            get { return (int)this.GetValue(CellSizeProperty); }
            set { this.SetValue(CellSizeProperty, value); }
        }
        public static readonly DependencyProperty CellSizeProperty = DependencyProperty.Register(
          "CellSize", typeof(int), typeof(CoordGridCanvas),
          new PropertyMetadata(50));

        public event PropertyChangedEventHandler PropertyChanged;

        public void Scale(decimal scale)
        {
            int newSize = (int)(CellSize * scale);
            CanvasViewPort = new Rect(
                new Point(-1 * (newSize / 2), -1 * (newSize / 2)),
                new Size(newSize, newSize));
            BrushGeometry = new Rect(new Point(0, 0), new Size(newSize, newSize));
            Centre(new Size(this.ActualWidth, this.ActualHeight));
        }

        public void Transform()
        {
            throw new NotImplementedException();
        }

        public void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Centre(e.NewSize);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static void OnScaleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as CoordGridCanvas)?.Scale((decimal)e.NewValue);
        }
    }
}
