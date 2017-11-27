using Lab3.Fractals.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab3.Fractals.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region fields
        private decimal _prevScale = 1.0M;
        #endregion

        public MainViewModel()
        {
            InitializeCommands();
            FractWidth = 250;
            Iteration = 0;

            Figures.Add(new CanvasPoint() { Ellipse = new Ellipse() { Height = 4, Width = 4, Stroke = Brushes.Red, StrokeThickness = 2 }, X = BuildPointX + Center.X, Y = Center.Y -BuildPointY });

            Figures.Add(new XAxis(new Line() { Stroke = Brushes.LightGray, StrokeThickness = 1 }));
            Figures.Add(new YAxis(new Line() { Stroke = Brushes.LightGray, StrokeThickness = 1 }));
        }


        #region Bindable
        private ObservableCollection<object> _figures = new ObservableCollection<object>();
        public ObservableCollection<object> Figures
        {
            get { return _figures; }
            set { _figures = value; OnPropertyChanged(); }
        }
        private Point _center;
        public Point Center
        {
            get { return _center; }
            set { _center = value; OnPropertyChanged(); }
        }
        private decimal _curScale;
        public decimal CanvasScale
        {
            get { return _curScale; }
            set { _curScale = value; OnPropertyChanged(); }
        }
        //private decimal _curScalingStep;
        //public decimal CanvasScalingStep
        //{
        //    get { return _curScalingStep; }
        //    set { _curScalingStep = value; Console.WriteLine(value); OnPropertyChanged(); }
        //}
        private int _buildPointX;
        public int BuildPointX
        {
            get { return _buildPointX; }
            set
            {
                _buildPointX = value;
                OnPropertyChanged();
                ((CanvasPoint)Figures.
                    Where(f => f.GetType() == typeof(CanvasPoint)).
                    DefaultIfEmpty(new CanvasPoint()
                    {
                        Ellipse = new Ellipse()
                        {
                            Height = 4,
                            Width = 4,
                            Stroke = Brushes.Red,
                            StrokeThickness = 2
                        },
                        X = BuildPointX + Center.X - 2,
                        Y = Center.Y - BuildPointY - 2
                    }).
                    First()).
                    X = BuildPointX + Center.X - 2; 
            }
        }
        private int _buildPointY;
        public int BuildPointY
        {
            get { return _buildPointY; }
            set
            {
                _buildPointY = value;
                OnPropertyChanged();
                ((CanvasPoint)Figures.
                    Where(f => f.GetType() == typeof(CanvasPoint)).
                    DefaultIfEmpty(new CanvasPoint()
                    {
                        Ellipse = new Ellipse()
                        {
                            Height = 4,
                            Width = 4,
                            Stroke = Brushes.Red,
                            StrokeThickness = 2
                        },
                        X = BuildPointX + Center.X - 2,
                        Y = Center.Y -BuildPointY - 2
                    }).
                    First()).
                    Y = Center.Y - BuildPointY - 2;
            }
        }
        private int _fractWidth;
        public int FractWidth
        {
            get { return _fractWidth; }
            set { _fractWidth = value; OnPropertyChanged(); }
        }
        private int _i;
        public int Iteration
        {
            get { return _i; }
            set { _i = value; OnPropertyChanged(); }
        }
        #endregion
        #region Commands
        public ICommand ScaleChangedCommand { get; private set; }
        
        public ICommand IncreaseIterationCommand { get; private set; }
        public ICommand DecreaseIterationCommand { get; private set; }
        public ICommand BuildCommand { get; private set; }
        void InitializeCommands()
        {
            ScaleChangedCommand = new DelegateCommand(p =>
            {
                double scale = (double)((decimal)p);
                double prev = (double)_prevScale;
                foreach (var item in Figures)
                {
                    if (item.GetType() == typeof(Line))
                        ((Line)item).RenderTransform = new ScaleTransform(scale, scale, Center.X, Center.Y);
                }
                _prevScale = (decimal)p;
            });

            BuildCommand = new DelegateCommand((p) =>
            {
                BuildPointX = BuildPointX;
                BuildPointY = BuildPointY;
                //Console.WriteLine(CanvasScale);
                CanvasScale = 1.0M;
                //_prevScale = 1.0M;

                var oldLines = Figures.Where(f => f.GetType() == typeof(Line)).ToList();
                foreach (var old in oldLines)
                    Figures.Remove(old);

                var lines = Builder.Build(Iteration, FractWidth, (int)(Center.X + BuildPointX), (int)(Center.Y - BuildPointY), Brushes.Green);
                foreach (var line in lines)
                    Figures.Add(line);

                //var step = CanvasScalingStep;
                while (true)
                {
                    var transform = new ScaleTransform((double)CanvasScale, (double)CanvasScale, Center.X, Center.Y);

                    var highestY = Figures.Where(f => f.GetType() == typeof(Line)).Min(l => ((Line)l).Y2);
                    var lowestY = Figures.Where(f => f.GetType() == typeof(Line)).Max(l => ((Line)l).Y1);

                    var lowestLeft = Figures.Where(f => f.GetType() == typeof(Line)).Min(l => ((Line)l).X1);
                    var greatestRight = Figures.Where(f => f.GetType() == typeof(Line)).Max(l => ((Line)l).X2);

                    var highestPoint = transform.Transform(new Point(0, highestY));
                    var lowestPoint = transform.Transform(new Point(0, lowestY));

                    var leftPoint = transform.Transform(new Point(lowestLeft, 0));
                    var rightPoint = transform.Transform(new Point(greatestRight, 0));

                    if (highestPoint.Y > 25 && lowestPoint.Y < Center.Y * 2 - 25 && leftPoint.X > 25 && rightPoint.X < Center.X * 2 - 25)
                        break;

                    foreach (var item in Figures)
                        if (item.GetType() == typeof(Line))
                        {
                            Line line = (Line)item;
                            line.RenderTransform = transform;
                        }
                        else
                        if(item.GetType() == typeof(CanvasPoint))
                        {
                            var np = transform.Transform(new Point(BuildPointX + Center.X, Center.Y - BuildPointY ));
                            ((CanvasPoint)item).X = np.X;
                            ((CanvasPoint)item).Y = np.Y;
                        }
                    //CanvasScale -= CanvasScalingStep;
                    CanvasScale -= 0.05M;
                }
            });

            IncreaseIterationCommand = new DelegateCommand((p) =>
            {
                Iteration++;
            });
            DecreaseIterationCommand = new DelegateCommand((p) =>
            {
                Iteration--;
            });
        }
        #endregion


        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
