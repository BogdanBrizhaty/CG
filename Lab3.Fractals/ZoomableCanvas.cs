using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Lab3.Fractals
{
    public class AxisInfo
    {
        public Line AxisLine { get; }
        public AxisInfo(Line line)
        {
            AxisLine = line;
        }
    }
    public class XAxis : AxisInfo
    {
        public XAxis(Line line) : base(line)
        {
        }
    }

    public class YAxis : AxisInfo
    {
        public YAxis(Line line) : base(line)
        {
        }
    }
    public class CanvasPoint : INotifyPropertyChanged
    {
        private double _x, _y;
        public double X
        {
            get { return _x; }
            set { _x = value; OnPropertyChanged(); }
        }
        public double Y
        {
            get { return _y; }
            set { _y = value; OnPropertyChanged(); }
        }
        public Ellipse Ellipse { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public sealed class CanvasLable
    {
        public string Text { get; }
        public double X { get; }
        public double Y { get; }
        public CanvasLable(string txt, double x, double y)
        {
            Text = txt;
            X = x;
            Y = y;
        }
    }
    public class ZoomableCanvas : Canvas
    {
        public ZoomableCanvas()
        {
            CurrentScaleChanged += Logger.Print;
            ScalingStep = 0.05M;
            CurrentScale = 1.0M;
        }

        public Point Center
        {
            get { return (Point)this.GetValue(CenterProperty); }
            set { this.SetValue(CenterProperty, value); }
        }

        public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(
          "Center", typeof(Point), typeof(ZoomableCanvas), new PropertyMetadata(new Point(0, 0)));

        //public DependencyObject ItemsSource
        //{
        //    get { return (DependencyObject)this.GetValue(ItemsSourceProperty); }
        //    set { this.SetValue(ItemsSourceProperty, value); }
        //}

        //public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
        //  "ItemsSource", typeof(DependencyObject), typeof(ZoomableCanvas), new PropertyMetadata(null));
        #region dependency properties
        public decimal CurrentScale
        {
            get { return (decimal)this.GetValue(CurrentScaleProperty); }
            set { this.SetValue(CurrentScaleProperty, value); }
        }

        public static readonly DependencyProperty CurrentScaleProperty = DependencyProperty.Register(
          "CurrentScale", typeof(decimal), typeof(ZoomableCanvas), new PropertyMetadata(1.00M));
        public decimal MinScale
        {
            get { return (decimal)this.GetValue(MinScaleProperty); }
            set { this.SetValue(MinScaleProperty, value); }
        }

        public static readonly DependencyProperty MinScaleProperty = DependencyProperty.Register(
          "MinScale", typeof(decimal), typeof(ZoomableCanvas), new PropertyMetadata(0.05M));
        public decimal MaxScale
        {
            get { return (decimal)this.GetValue(MaxScaleProperty); }
            set { this.SetValue(MaxScaleProperty, value); }
        }

        public static readonly DependencyProperty MaxScaleProperty = DependencyProperty.Register(
          "MaxScale", typeof(decimal), typeof(ZoomableCanvas), new PropertyMetadata(2.00M));
        public decimal ScalingStep
        {
            get { return (decimal)this.GetValue(ScalingStepProperty); }
            set { this.SetValue(ScalingStepProperty, value); }
        }

        public static readonly DependencyProperty ScalingStepProperty = DependencyProperty.Register(
          "ScalingStep", typeof(decimal), typeof(ZoomableCanvas), new PropertyMetadata(0.05M));
        public Point CenterPoint
        {
            get { return (Point)this.GetValue(CenterPointProperty); }
            set { this.SetValue(CenterPointProperty, value); }
        }

        public static readonly DependencyProperty CenterPointProperty = DependencyProperty.Register(
          "CenterPoint", typeof(Point), typeof(ZoomableCanvas), new PropertyMetadata(new Point(0,0)));
        #endregion

        #region custom events

        public event CurrentScaleChangedEventHandler CurrentScaleChanged;
        public delegate void CurrentScaleChangedEventHandler(ScaleChangedEventArgs e);

        public event ChildrenCollectionChangedEventHandler ChildrenCollectionChanged;
        public delegate void ChildrenCollectionChangedEventHandler(CollectionChangedEventArgs e);


        protected virtual void OnScaleChanged(ScaleChangedEventArgs e)
        {
            //for (int i = 0; i < )
            //this.Pa
            Console.WriteLine(this.Parent.DependencyObjectType.ToString());
        }

        #endregion

        #region override block
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            var old = CurrentScale;
            if (e.Delta > 0)
                CurrentScale += (CurrentScale + ScalingStep <= MaxScale) ? ScalingStep : 0;
            if (e.Delta < 0)
                CurrentScale -= (CurrentScale - ScalingStep >= MinScale) ? ScalingStep : 0;
            CurrentScaleChanged?.Invoke(new ScaleChangedEventArgs(old, CurrentScale, this.Name));
            base.OnMouseWheel(e);
        }
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            ChildrenCollectionChanged?.Invoke(new CollectionChangedEventArgs(visualAdded, visualRemoved, this.Name + "Children"));
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            Center = new Point(ActualWidth / 2, ActualHeight / 2);
            base.OnRenderSizeChanged(sizeInfo);
        }
        #endregion
    }
    public class MyItCtrl : ItemsControl
    {
        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
        }
    }
}
