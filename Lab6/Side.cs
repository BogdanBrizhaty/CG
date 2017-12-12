using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lab6
{
    public class Side
    {
        private Marker _startAt = null;
        private Marker _endAt = null;

        public Marker StartAt
        {
            get { return _startAt; }
        }
        public Marker EndAt
        {
            get { return _endAt; }
        }

        public SolidColorBrush Color { get; private set; }
        public int Thickness { get; private set; }

        public Side(Marker st, Marker end)
        {
            Color = Brushes.Black;
            Thickness = 1;
            _startAt = st;
            _endAt = end;
        }
    }
}
