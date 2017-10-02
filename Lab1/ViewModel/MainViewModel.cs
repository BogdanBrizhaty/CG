using Lab1.Model;
using Lab1.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab1.ViewModel
{
    class MainViewModel : BaseNotifyingModel
    {
        #region fields
        private Rect _canvasViewport = new Rect(new Point(-12, -12), new Size(25, 25));
        private int _defaultSize = 50;
        private decimal _defaultScale = 1.00M;
        private Rect _brushGeometry = new Rect(new Point(0, 0), new Size(25, 25));
        private decimal _scale = 1.00M;
        private bool _autoScaling = true;

        //COMMANDS
        //private ICommand _increaseScaleButtonPressedCommand;
        #endregion
        
        public MainViewModel()
        {
            int newSize = (int)(_defaultSize * _scale);
            //CanvasViewPort = new Rect(
            //    new Point(-1 * (newSize / 2), -1 * (newSize / 2)), 
            //    new Size(newSize, newSize));
            //BrushGeometry = new Rect(new Point(0, 0), new Size(newSize, newSize));

            InitializeCommands();
        }



        private decimal _bindTest = 0.25M;
        public decimal BindTest
        {
            get { return _bindTest; }
            set
            {
                _bindTest = value;
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
        //public ICommand IncreaseScaleButtonPressedCommand { get; private set; }
        //public ICommand DecreaseScaleButtonPressedCommand { get; private set; }
        #endregion

        #region command initializer
        void InitializeCommands()
        {
            //IncreaseScaleButtonPressedCommand = new IncScaleCommand();
            //DecreaseScaleButtonPressedCommand = new DecScaleCommand();
        }
        #endregion
    }
}
