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

        private int _yArrowLeftLine = 0;
        private int _yArrowRightLine = 0;
        private int _xArrowLeftPosLine = 0;
        private int _xArrowTopLine = 0;
        private int _xArrowBottomLine = 0;

        public int XArrowLeftPosLine
        {
            get { return _xArrowLeftPosLine; }
            set
            {
                _xArrowLeftPosLine = value;
                OnPropertyChanged("xArrowLeftPosLine");
            }
        }
        public int XArrowTopLine
        {
            get { return _xArrowTopLine; }
            set
            {
                _xArrowTopLine = value;
                OnPropertyChanged("xArrowTopLine");
            }
        }
        public int XArrowBottomLine
        {
            get { return _xArrowBottomLine; }
            set
            {
                _xArrowBottomLine = value;
                OnPropertyChanged("xArrowBottomLine");
            }
        }
        public int YArrowLeftLine
        {
            get { return _yArrowLeftLine; }
            set
            {
                _yArrowLeftLine = value;
                OnPropertyChanged("YArrowLeftLine");
            }
        }
        public int YArrowRightLine
        {
            get { return _yArrowRightLine; }
            set
            {
                _yArrowRightLine = value;
                OnPropertyChanged("YArrowRightLine");
            }
        }
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

        public CoordGridCanvas()
        {
            InitializeComponent();
            Centre(new Size(this.Height, this.Width));

            int newSize = (int)(CellSize * CurrentScale);
            CanvasViewPort = new Rect(
                new Point(-1 * (newSize / 2), -1 * (newSize / 2)),
                new Size(newSize, newSize));
            BrushGeometry = new Rect(new Point(0, 0), new Size(newSize, newSize));
            CreateMarks();
        }
        void CreateMarks()
        {
            //CurrentScaleChanged += Logger.Log;

            TextBlock elem = null;
            for (int i = 1; i < 55; i++)
            {
                elem = new TextBlock() { Text = (i * 10).ToString(), Name = "xMark" + i };
                Canvas.SetTop(elem, i * 10);
                Canvas.SetLeft(elem, 25);
                this.canvas.Children.Add(elem);
                elem.BringToFront();

                elem = new TextBlock() { Text = (i * 10).ToString(), Name = "yMark" + i };
                Canvas.SetTop(elem, 25);
                Canvas.SetLeft(elem, i * 10);
                elem.BringToFront();
                this.canvas.Children.Add(elem);
            }
        }
        void pickUpScale()
        {
            double maxSize = Figures.Max(f => f.DefaultSize.Width);

            double maxX = (double)CurrentScale * CanvasToGridPointsRatio * (Figures.Max(f => f.DefaultX) + maxSize * 0.75);
            double maxY = (double)CurrentScale * CanvasToGridPointsRatio * (Figures.Max(f => f.DefaultY) + maxSize * 0.75);
            double minX = (double)CurrentScale * CanvasToGridPointsRatio * (Figures.Min(f => f.DefaultX) - maxSize * 0.75);
            double minY = (double)CurrentScale * CanvasToGridPointsRatio * (Figures.Min(f => f.DefaultY) - maxSize * 0.75);
            maxSize = Math.Max(maxX, maxY);
            maxSize = Math.Max(maxSize, Math.Abs(minX));
            maxSize = Math.Max(maxSize, Math.Abs(minY));

            int newSize = (int)(CellSize * CurrentScale);

            var cWidth = (int)(this.canvas.ActualWidth / newSize) / 2;
            var cHeight = (int)(this.canvas.ActualHeight / newSize) / 2;
            maxSize /= newSize;

            while (CurrentScale > 0.1M && maxSize > cHeight && maxSize > cWidth && maxSize != 2)
            {
                //Console.WriteLine("----------");
                maxSize = Figures.Max(f => f.DefaultSize.Width);
                maxX = (double)CurrentScale * CanvasToGridPointsRatio * (Figures.Max(f => f.DefaultX) + maxSize * 0.75);
                maxY = (double)CurrentScale * CanvasToGridPointsRatio * (Figures.Max(f => f.DefaultY) + maxSize * 0.75);
                minX = (double)CurrentScale * CanvasToGridPointsRatio * (Figures.Min(f => f.DefaultX) - maxSize * 0.75);
                minY = (double)CurrentScale * CanvasToGridPointsRatio * (Figures.Min(f => f.DefaultY) - maxSize * 0.75);

                maxSize = Math.Max(maxX, maxY);
                maxSize = Math.Max(maxSize, Math.Abs(minX));
                maxSize = Math.Max(maxSize, Math.Abs(minY));

                //Console.WriteLine("maxSize " + maxSize);

                newSize = (int)(CellSize * CurrentScale);

                cWidth = (int)(this.canvas.ActualWidth / newSize) / 2;
                cHeight = (int)(this.canvas.ActualHeight / newSize) / 2;

                maxSize /= newSize;
                //Console.WriteLine("maxSize " + maxSize);

                CurrentScale -= 0.1M;
            }

            //Console.WriteLine("scale " + CurrentScale.ToString());
        }
        public void Centre(Size e)
        {
            CoordCentre = new Point((int)(e.Width / 2), (int)(e.Height / 2));
            yTextBlockPosition = (int)CoordCentre.X - 10;

            YArrowLeftLine = (int)(CoordCentre.X - 5);
            YArrowRightLine = (int)(CoordCentre.X + 5);

            XArrowLeftPosLine = (int)(canvas.ActualWidth - 10);
            XArrowBottomLine = (int)(CoordCentre.Y + 5);
            XArrowTopLine = (int)(CoordCentre.Y - 5);

            // marks working
            foreach (UIElement el  in canvas.Children)
            {
                if (el is TextBlock && (el as TextBlock).Name.StartsWith("yMark"))
                {
                    if ((CoordCentre.Y
                        - (int)(Int32.Parse((el as TextBlock).Text) * (CellSize / CanvasToGridPointsRatio) * CurrentScale)
                        - (el as TextBlock).ActualHeight / 2) < 20)
                    {
                        el.Visibility = Visibility.Hidden;
                        continue;
                    }
                    else
                        el.Visibility = Visibility.Visible;

                    if (CurrentScale <= 0.2M)
                        (el as TextBlock).FontSize = 10;
                    else
                        (el as TextBlock).FontSize = 12;

                    Canvas.SetTop(el, CoordCentre.Y
                        - (int)(Int32.Parse((el as TextBlock).Text) * (CellSize / CanvasToGridPointsRatio) * CurrentScale)
                        - (el as TextBlock).ActualHeight / 2);
                    Canvas.SetLeft(el, CoordCentre.X - (el as TextBlock).ActualWidth - 5);
                }
                if (el is TextBlock && (el as TextBlock).Name.StartsWith("xMark"))
                {
                    if ((CoordCentre.X
                        + (int)(Int32.Parse((el as TextBlock).Text) * (CellSize / CanvasToGridPointsRatio) * CurrentScale)
                        - (el as TextBlock).ActualWidth / 2) > canvas.ActualWidth - 30)
                    {
                        el.Visibility = Visibility.Hidden;
                        continue;
                    }
                    else
                        el.Visibility = Visibility.Visible;

                    if (CurrentScale <= 0.2M)
                        (el as TextBlock).FontSize = 7;
                    else
                        (el as TextBlock).FontSize = 12;

                    Canvas.SetLeft(el, CoordCentre.X
                        + (int)(Int32.Parse((el as TextBlock).Text) * (CellSize / CanvasToGridPointsRatio) * CurrentScale)
                        - (el as TextBlock).ActualWidth / 2);
                    Canvas.SetTop(el, CoordCentre.Y + 5);
                }
            }

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
                    -1 * (newSize / 2) + heightOffset),
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
            set
            {
                var oldScale = (decimal)this.GetValue(CurrentScaleProperty);
                this.SetValue(CurrentScaleProperty, value);
            }
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

        /// <summary>
        /// Is using for resizing figures due to Scale changes
        /// </summary>
        /// <param name="scale"></param>
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
        /// <summary>
        /// Moves figures to new positions
        /// </summary>
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
        /// <summary>
        /// Handler to detect canvas size changes. Centres axes and grid, and calls Transfrom action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Centre(e.NewSize);
            Transform(e.NewSize, e.PreviousSize);
        }
        /// <summary>
        /// implementation of INotifyPropertyChanged interface
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Event handler that handles the changes in CurrentScale DP and calls rescaling action
        /// </summary>
        public static void OnScaleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as CoordGridCanvas)?.Scale((decimal)e.NewValue);
            //CurrentScaleChanged?.Invoke(new LogEventArgs(String.Format("{0} - Scale changed from [old] {1} to [new] {2}", DateTime.Now.ToShortTimeString(), e.OldValue, e.NewValue)));

        }
        /// <summary>
        /// Paint the figure when it's added to Figures collection
        /// </summary>
        /// <param name="f" type="Model.Figure">Figure to paint</param>
        void PaintFigure(Model.Figure f)
        {
            if (f == null)
                return;

            f.Rect.Height *= (double)(CanvasToGridPointsRatio * CurrentScale);
            f.Rect.Width *= (double)(CanvasToGridPointsRatio * CurrentScale);
            Canvas.SetTop(f.Rect, CoordCentre.Y - CanvasToGridPointsRatio * (double)(f.Y * CurrentScale));
            Canvas.SetLeft(f.Rect, (CoordCentre.X + CanvasToGridPointsRatio * (double)(f.X * CurrentScale)));
            canvas.Children.Add(f.Rect);

            f.Ellipse.Height *= (double)(CanvasToGridPointsRatio * CurrentScale);
            f.Ellipse.Width *= (double)(CanvasToGridPointsRatio * CurrentScale);
            Canvas.SetTop(f.Ellipse, CoordCentre.Y - CanvasToGridPointsRatio * (double)(f.Y * CurrentScale));
            Canvas.SetLeft(f.Ellipse, CoordCentre.X + CanvasToGridPointsRatio * (double)(f.X * CurrentScale));
            canvas.Children.Add(f.Ellipse);

            // scale to suitable size
            pickUpScale();
        }

        #region KPZ event lab(2)
        public static event FiguresCollectionChangedEventHandler FiguresCollectionChanged;
        public delegate void FiguresCollectionChangedEventHandler(LogEventArgs e);
        public class LogEventArgs : EventArgs
        {
            public string LogMessage { get; protected set; }
            public LogEventArgs()
            {

            }
            public LogEventArgs(string msg)
            {
                LogMessage = msg;
            }
        }
        class FiguresCollectionChangedEventArgs : LogEventArgs
        {
            public NotifyCollectionChangedAction ActionType { get; protected set; }
            public int NewElementsCount { get; protected set; }
            public FiguresCollectionChangedEventArgs(NotifyCollectionChangedAction action, int newSize)
            {
                ActionType = action;
                NewElementsCount = newSize;
                LogMessage = String.Format("{0} - Collection has changed with {1} action, now it containst {2} items", DateTime.Now.ToShortTimeString(), ActionType, NewElementsCount);
            }
        }

        #endregion


        /// <summary>
        /// Listens to the changes in Figures Collection DP and sets the tracker on new value
        /// and removes the old one from previous
        /// </summary>
        private static void OnFiguresCollectionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var @new = e.NewValue as ObservableCollection<Model.Figure>;
                
                var old = e.NewValue as ObservableCollection<Model.Figure>;
                //Console.WriteLine(old.Any());
                //Console.WriteLine(e.NewValue.GetType());
            }
            // tracker
            var action = new NotifyCollectionChangedEventHandler(
                    (o, args) =>
                    {

                        var grid = obj as CoordGridCanvas;

                        if (grid != null)
                        {
                            var arg = args as NotifyCollectionChangedEventArgs;
                            // call log event handler
                            FiguresCollectionChanged?.Invoke(new FiguresCollectionChangedEventArgs(arg.Action, (o as ObservableCollection<Model.Figure>).Count));

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
            // tracker removing
            if (e.OldValue != null)
            {
                var coll = (INotifyCollectionChanged)e.OldValue;
                coll.CollectionChanged -= action;
            }
            // tracker binding
            if (e.NewValue != null)
            {
                var coll = (ObservableCollection<Model.Figure>)e.NewValue;
                coll.CollectionChanged += action;
            }
        }

        /// <summary>
        /// Made wheel-scrolling available
        /// </summary>
        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                CurrentScale += (CurrentScale + 0.10M <= 2.50M) ? 0.10M : 0;
            if (e.Delta < 0)
                CurrentScale -= (CurrentScale - 0.10M > 0) ? 0.10M : 0;
        }
    }
}
