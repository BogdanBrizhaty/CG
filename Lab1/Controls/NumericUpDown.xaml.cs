using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1.Controls
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
            //textBox.Text = Value.ToString();
        }
        public decimal HighestValue
        {
            get { return (decimal)this.GetValue(HighestValueProperty); }
            set { this.SetValue(HighestValueProperty, value); }
        }

        public static readonly DependencyProperty HighestValueProperty = DependencyProperty.Register(
          "HighestValue", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(5.00M));
        public decimal LowestValue
        {
            get { return (decimal)this.GetValue(LowestValueProperty); }
            set { this.SetValue(LowestValueProperty, value); }
        }

        public static readonly DependencyProperty LowestValueProperty = DependencyProperty.Register(
          "LowestValue", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(0.00001M));
        public decimal Value
        {
            get { return (decimal)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }
        
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
          "Value", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(0.00M));

        public decimal Step
        {
            get { return (decimal)this.GetValue(StepProperty); }
            set { this.SetValue(StepProperty, value); }
        }
        public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
          "Step", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(1.00M));

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Value + Step <= HighestValue)
            {
                button1.IsEnabled = true;
                Value += Step;
            }
            else
                button.IsEnabled = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (Value - Step >= LowestValue)
            {
                button.IsEnabled = true;
                Value -= Step;
            }
            else
                button1.IsEnabled = false;
        }
    }
}
