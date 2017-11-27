using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Fractals
{
    public class CollectionChangedEventArgs : LogEventArgs
    {
        public object Added { get; private set; }
        public object Removed { get; private set; }
        public CollectionChangedEventArgs(object added, object removed, string collectionName = null)
        {
            Added = added;
            Removed = removed;

            LogMessage = String.Format("{0} --- Collection {1} changed with {2} added and {3} removed",
                DateTime.Now.ToLongTimeString(),
                collectionName == "" ? "[Name not set]" : collectionName,
                Removed,
                Added);
        }
    }
}
