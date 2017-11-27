using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Fractals
{
    public static class Logger
    {
        public static string AssemblyLocation
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        public static void Print(LogEventArgs log)
        {
            Console.WriteLine(log.LogMessage);
        }
        public static void ToFile(LogEventArgs log)
        {
            try
            {
                File.AppendAllText(Logger.AssemblyLocation, log.LogMessage);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
