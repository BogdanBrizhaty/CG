using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab1.Controls.CoordGridCanvas;

namespace Lab1
{
    public class Logger
    {
        public static void Log(LogEventArgs args)
        {
            Console.WriteLine(args.LogMessage);
        }
    }
}
