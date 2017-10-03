using Lab1.Model;
using Lab1.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
        private int _x1, _x2, _y1, _y2;

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

        public ObservableCollection<Model.Figure> Figures
        {
            get { return _figures; }
            set
            {
                _figures = value;
                //OnCollectionChanged
                OnPropertyChanged("Figures");
            }
        }



        private decimal _bindTest = 0.25M;
        public decimal BindTest
        {
            get { return _bindTest; }
            set
            {
                _bindTest = value;
                Console.WriteLine("property");
                OnPropertyChanged("BindTest");
            }
        }

        #region properties
        public decimal ScalingStep { get { return 0.1M; } }
        public int CellSize { get { return _defaultSize; } }
        // bindable
        //public Rect CanvasViewPort
        //{
        //    get { return _canvasViewport; }
        //    set
        //    {
        //        _canvasViewport = value;
        //        OnPropertyChanged("CanvasViewPort");
        //    }
        //}

        //public Rect BrushGeometry
        //{
        //    get { return _brushGeometry; }
        //    set
        //    {
        //        _brushGeometry = value;
        //        OnPropertyChanged("BrushGeometry");
        //    }
        //}

        public decimal Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                //Console.WriteLine(value);
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
