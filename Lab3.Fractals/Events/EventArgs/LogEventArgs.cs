using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Fractals
{
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
}
