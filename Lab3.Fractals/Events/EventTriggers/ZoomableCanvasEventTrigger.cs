using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interactivity;

namespace Lab3.Fractals
{
    public class ZoomableCanvasEventTrigger : TriggerBase<ZoomableCanvas>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.CurrentScaleChanged += OnScaleChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.CurrentScaleChanged -= OnScaleChanged;

        }

        protected void OnScaleChanged(ScaleChangedEventArgs e)
        {
            this.InvokeActions(e);
        }

        //
        // To invoke any associated Actions when this Trigger gets called, use
        // this.InvokeActions(o) where o is an object that you can pass in as a parameter
        //
    }
}
