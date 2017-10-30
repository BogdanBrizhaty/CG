using Lab1.Controls;
using Lab1.Model;
using Lab1.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static Lab1.Controls.CoordGridCanvas;

namespace Lab1.ViewModel
{
    class MainViewModel : BaseNotifyingModel
    {
        #region fields
        private Rect _canvasViewport = new Rect(new Point(-12, -12), new Size(25, 25));
        private int _defaultSize = 100;
        private decimal _defaultScale = 1.00M;
        private Rect _brushGeometry = new Rect(new Point(0, 0), new Size(25, 25));
        private decimal _scale = 1.00M;
        private bool _autoScaling = true;
        private ObservableCollection<Model.Figure> _figures;
        private int _x1 = 50, _x2 = -10, _y1 = 140, _y2 = 25;

        private Model.Figure _selectedFigure = null;
        public Model.Figure SelectedFigure
        {
            get { return _selectedFigure; }
            set
            {
                _selectedFigure = value;
                //Console.WriteLine("selected: " + value.X);
                OnPropertyChanged("SelectedFigure");
            }
        }
        public int X1
        {
            get { return _x1; }
            set
            {
                _x1 = value;
                OnPropertyChanged("X1");
            }
        }
        public List<BrushInfo> Brushes { get; private set; }
        public int X2
        {
            get { return _x2; }
            set
            {
                _x2 = value;
                OnPropertyChanged("X2");
            }
        }
        public int Y1
        {
            get { return _y1; }
            set
            {
                _y1 = value;
                OnPropertyChanged("Y1");
            }
        }
        public int Y2
        {
            get { return _y2; }
            set
            {
                _y2 = value;
                OnPropertyChanged("Y2");
            }
        }
        private BrushInfo _selectedRectColor;
        private BrushInfo _selectedEllipseColor;

        public BrushInfo SelectedRectColor
        {
            get { return _selectedRectColor; }
            set
            {
                _selectedRectColor = value;
                OnPropertyChanged("SelectedRectColor");
            }
        }
        public BrushInfo SelectedEllipseColor
        {
            get { return _selectedEllipseColor; }
            set
            {
                _selectedEllipseColor = value;
                OnPropertyChanged("SelectedEllipseColor");
            }
        }
        //COMMANDS
        //private ICommand _increaseScaleButtonPressedCommand;
        #endregion
        private bool _log = false;
        public bool LogEnabled
        {
            get { return _log; }
            set
            {
                if (value)
                {
                    CurrentScaleChanged += Logger.Log;
                    CoordGridCanvas.FiguresCollectionChanged += Logger.Log;
                }
                else
                {
                    CurrentScaleChanged -= Logger.Log;
                    CoordGridCanvas.FiguresCollectionChanged -= Logger.Log;
                }

                _log = value;
                OnPropertyChanged("LogEnabled");
            }
        }
        public MainViewModel()
        {
            Brushes = new List<BrushInfo>()
            {
                new BrushInfo("Black", System.Windows.Media.Brushes.Black),
                new BrushInfo("Red", System.Windows.Media.Brushes.Red),
                new BrushInfo("Blue", System.Windows.Media.Brushes.Blue),
                new BrushInfo("Green", System.Windows.Media.Brushes.Green),
                new BrushInfo("Yellow", System.Windows.Media.Brushes.Yellow),
                new BrushInfo("DarkGray", System.Windows.Media.Brushes.DarkGray),
            };
            SelectedRectColor = Brushes.First();
            SelectedEllipseColor = Brushes.First();
            int newSize = (int)(_defaultSize * _scale);
            Figures = new ObservableCollection<Figure>();
            InitializeCommands();

        }

        public static event CurrentScaleChangedEventHandler CurrentScaleChanged;
        public delegate void CurrentScaleChangedEventHandler(LogEventArgs e);

        public ObservableCollection<Model.Figure> Figures
        {
            get { return _figures; }
            set
            {
                _figures = value;
                OnPropertyChanged("Figures");
            }
        }

        #region properties
        public decimal ScalingStep { get { return 0.1M; } }
        public int CellSize { get { return _defaultSize; } }

        public decimal Scale
        {
            get { return _scale; }
            set
            {
                CurrentScaleChanged?.Invoke(new LogEventArgs(String.Format("{0} - Scale changed from [old] {1} to [new] {2}", DateTime.Now.ToShortTimeString(), _scale, value)));
                _scale = value;
                OnPropertyChanged("Scale");
            }
        }

        // COMMANDS
        public ICommand AddFigureCommand { get; private set; }
        public ICommand RemoveFigureCommand { get; private set; }
        public ICommand EditColorCommand { get; private set; }
        #endregion

        #region command initializer
        void InitializeCommands()
        {
            AddFigureCommand = new AddFigureCommand();
            RemoveFigureCommand = new RemoveFigureCommand();
            EditColorCommand = new EditColorCommand();
        }
        #endregion
    }
}
