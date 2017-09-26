using Lab1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab1.ViewModel
{
    class MainViewModel : BaseNotifyingModel
    {
        private Rect _canvasViewport = new Rect(new Point(-25, -25), new Size(50, 50));
        public MainViewModel()
        {
            Console.WriteLine("crer");
            //CanvasViewPort = new Rect(new Point(0, 0), new Size(50, 50));
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
    }
}
