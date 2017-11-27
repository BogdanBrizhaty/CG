using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Fractals
{
    public class ScaleChangedEventArgs : LogEventArgs
    {
        public decimal OldScale { get; private set; }
        public decimal NewScale { get; private set; }
        public ScaleChangedEventArgs(decimal old, decimal @new,[CallerMemberName] string elementName = null)
        {
            OldScale = old;
            NewScale = @new;
            LogMessage = String.Format("{0} --- Scale of {1} changed from {2} to {3}", 
                DateTime.Now.ToLongTimeString(), 
                elementName == "" ? "[Name not set]" : elementName, 
                OldScale, 
                NewScale);
        }
    }
}
