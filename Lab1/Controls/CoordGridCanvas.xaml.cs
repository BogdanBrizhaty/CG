using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        private int _yTextBlockPosition = 0;

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
            Centre(new Size(this.Height, this.Width));

            int newSize = (int)(CellSize * CurrentScale);
            CanvasViewPort = new Rect(
                new Point(-1 * (newSize / 2), -1 * (newSize / 2)),
                new Size(newSize, newSize));
            BrushGeometry = new Rect(new Point(0, 0), new Size(newSize, newSize));
            //Figures = new List<Model.Figure>();
            //Figures.Add(new Model.Figure()
            //{
            //    X = 10, Y = 10,
            //    Rect = new Rectangle()
            //    {
            //        Height = 10,
            //        Width = 10,
            //        Stroke = Brushes.Black,
            //        StrokeThickness = 1
            //    },
            //    Ellipse = new Ellipse()
            //    {
            //        Width = 15,
            //        Height = 15,
            //        Stroke = Brushes.Black,
            //        StrokeThickness = 1
            //    }
            //});
        }
        public void Centre(Size e)
        {
            // new code
            CoordCentre = new Point((int)(e.Width / 2), (int)(e.Height / 2));
            yTextBlockPosition = (int)CoordCentre.X - 10;

            int newSize = (int)(CellSize * CurrentScale);

            var cWidth = (newSize * (int)(e.Width / newSize)) / 2;
            var cHeight = (newSize * (int)(e.Height / newSize)) / 2;
            var widthOffset = (int)(e.Width / 2) - cWidth;
            var heightOffset = (int)(e.Height / 2) - cHeight;

            if (cWidth % newSize == 0)
                widthOffset += newSize / 2;
            if (cHeight % newSize == 0)
                heightOffset += newSize / 2;
            CanvasViewPort = new Rect(
                new Point(-1 * (newSize / 2) + widthOffset,
                    - 1 * (newSize / 2) + heightOffset),
                new Size(newSize, newSize));
            BrushGeometry = new Rect(new Point(0, 0), new Size(newSize, newSize));
        }
        public int yTextBlockPosition
        {
            get { return _yTextBlockPosition; }
            set
            {
                _yTextBlockPosition = value;
                OnPropertyChanged("yTextBlockPosition");
            }
        }
        //public Point CoordCentre { get; set; }
        public Point CoordCentre
        {
            get { return (Point)this.GetValue(CoordCentreProperty); }
            set
            {
                var oldValue = CoordCentre;
                this.SetValue(CoordCentreProperty, value);
                OnPropertyChanged(new DependencyPropertyChangedEventArgs(CoordCentreProperty, oldValue, value));
            }
        }
        public static readonly DependencyProperty CoordCentreProperty = DependencyProperty.Register(
          "CoordCentre", typeof(Point), typeof(CoordGridCanvas),
          new PropertyMetadata(new Point(0, 0)));

        private int _defaultRatio = 10;
        private int _ratio = 10;
        public int CanvasToGridPointsRatio
        {
            get { return _ratio; }
            set { _ratio = value; }
        }

        public decimal CurrentScale
        {
            get { return (decimal)this.GetValue(CurrentScaleProperty); }
            set { this.SetValue(CurrentScaleProperty, value); }
        }
        public static readonly DependencyProperty CurrentScaleProperty = DependencyProperty.Register(
          "CurrentScale", typeof(decimal), typeof(CoordGridCanvas),
          new PropertyMetadata(1.00M, new PropertyChangedCallback(OnScaleChanged)));

        public ObservableCollection<Model.Figure> Figures
        {
            get { return (ObservableCollection<Model.Figure>)this.GetValue(FiguresProperty); }
            set { this.SetValue(FiguresProperty, value); }
        }
        public static readonly DependencyProperty FiguresProperty = DependencyProperty.Register(
          "Figures", typeof(ObservableCollection<Model.Figure>), typeof(CoordGridCanvas),
          new PropertyMetadata(null, new PropertyChangedCallback(OnFiguresCollectionChanged)));

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
            Centre(new Size(this.ActualWidth, this.ActualHeight));

            if (Figures == null)
                return;

            foreach (UIElement item in canvas.Children)
            {
                var figure = Figures.Where(f => f.Rect == item || f.Ellipse == item).FirstOrDefault();
                if (figure == null)
                    continue;
                if (typeof(Rectangle) == item.GetType())
                {
                    (item as Rectangle).Width = (int)((decimal)figure.DefaultSize.Width * CanvasToGridPointsRatio * CurrentScale);
                    (item as Rectangle).Height = (int)((decimal)figure.DefaultSize.Height * CanvasToGridPointsRatio * CurrentScale);
                }
                if (typeof(Ellipse) == item.GetType())
                {
                    // move ellipse
                    (item as Ellipse).Width = (int)((decimal)figure.DefaultSize.Width * CanvasToGridPointsRatio * CurrentScale);
                    (item as Ellipse).Height = (int)((decimal)figure.DefaultSize.Height * CanvasToGridPointsRatio * CurrentScale);
                }

                Canvas.SetTop(item, CoordCentre.Y - CanvasToGridPointsRatio * (double)(figure.Y * CurrentScale));
                Canvas.SetLeft(item, CoordCentre.X + CanvasToGridPointsRatio * (double)(figure.X * CurrentScale));
            }
        }

        public void Transform(Size newSize, Size oldSize)
        {
            if (Figures == null || Figures.Count == 0)
                return;

            foreach (UIElement item in canvas.Children)
            {
                var figure = Figures.Where(f => f.Rect == item || f.Ellipse == item).FirstOrDefault();
                if (figure == null)
                    continue;
                Canvas.SetTop(item, CoordCentre.Y - CanvasToGridPointsRatio * (double)(figure.Y * CurrentScale) /*(CoordCentre.Y - CanvasToGridPointsRatio * (figure.Y) - figure.Rect.Height)*/);
                Canvas.SetLeft(item, CoordCentre.X + CanvasToGridPointsRatio * (double)(figure.X * CurrentScale));
            }
        }

        public void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Centre(e.NewSize);
            Transform(e.NewSize, e.PreviousSize);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static void OnScaleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as CoordGridCanvas)?.Scale((decimal)e.NewValue);
        }
        void PaintFigure(Model.Figure f)
        {
            if (f == null)
                return;
            f.Rect.Height *= CanvasToGridPointsRatio;
            f.Rect.Width *= CanvasToGridPointsRatio;
            Canvas.SetTop(f.Rect, CoordCentre.Y - CanvasToGridPointsRatio * (f.Y));
            Canvas.SetLeft(f.Rect, (CoordCentre.X + CanvasToGridPointsRatio * f.X));
            canvas.Children.Add(f.Rect);

            f.Ellipse.Height *= CanvasToGridPointsRatio;
            f.Ellipse.Width *= CanvasToGridPointsRatio;
            Canvas.SetTop(f.Ellipse, CoordCentre.Y - CanvasToGridPointsRatio * (int)(f.Y * CurrentScale));
            Canvas.SetLeft(f.Ellipse, CoordCentre.X + CanvasToGridPointsRatio * (int)(f.X * CurrentScale));
            canvas.Children.Add(f.Ellipse);
        }

        private void ctrl1_Loaded(object sender, RoutedEventArgs e)
        {
            //PaintFigure(Figures.LastOrDefault());
        }
        public void AddFigure(Model.Figure f)
        {
            if (f == null)
                throw new ArgumentNullException();

            if (Figures == null)
                Figures = new ObservableCollection<Model.Figure>();
            // blah blah
            Figures.Add(f);
            PaintFigure(f);
            // paint
        }

        private static void OnFiguresCollectionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var @new = e.NewValue as ObservableCollection<Model.Figure>;
                
                var old = e.NewValue as ObservableCollection<Model.Figure>;
                Console.WriteLine(old.Any());
                Console.WriteLine(e.NewValue.GetType());
            }
            var action = new NotifyCollectionChangedEventHandler(
                    (o, args) =>
                    {
                        var grid = obj as CoordGridCanvas;

                        if (grid != null)
                        {
                            var arg = args as NotifyCollectionChangedEventArgs;
                            if (arg.NewItems != null && arg.NewItems.Count > 0)
                                grid.PaintFigure(arg.NewItems[0] as Model.Figure);
                            if (arg.Action == NotifyCollectionChangedAction.Remove)
                            {
                                var removable = (arg.OldItems[0] as Model.Figure);
                                grid.canvas.Children.Remove(removable.Rect);
                                grid.canvas.Children.Remove(removable.Ellipse);
                            }
                        }
                    });

            if (e.OldValue != null)
            {
                var coll = (INotifyCollectionChanged)e.OldValue;
                coll.CollectionChanged -= action;
            }

            if (e.NewValue != null)
            {
                var coll = (ObservableCollection<Model.Figure>)e.NewValue;
                coll.CollectionChanged += action;
            }
        }

        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                CurrentScale += (CurrentScale + 0.10M <= 2.50M) ? 0.10M : 0;
            if (e.Delta < 0)
                CurrentScale -= (CurrentScale - 0.10M > 0) ? 0.10M : 0;
        }
    }
}
