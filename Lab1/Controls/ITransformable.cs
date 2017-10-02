using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab1.Controls
{
    public interface ITransformable
    {
        void Transform(Size newSize, Size oldSize);
    }
}
