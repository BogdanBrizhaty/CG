using Lab1.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
        private static void OnFiguresCollectionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("changed col");
            var action = new NotifyCollectionChangedEventHandler(
                    (o, args) =>
                    {
                        var grid = obj as MainWindow;

                        if (grid != null)
                        {
                            //Console.WriteLine("count: " + grid.figuresListing.Items.Count);
                            var arg = args as NotifyCollectionChangedEventArgs;
                            //if (arg.NewItems != null)
                                //grid.
                            //grid.figuresListing.ItemsSource
                            //ItemContainerStyle="{StaticResource ResourceKey=CustomListViewItem}"
                            //grid.L
                            //grid.Figures
                        }
                    });

            if (e.OldValue != null)
            {
                var coll = (INotifyCollectionChanged)e.OldValue;
                coll.CollectionChanged -= action;
            }

            if (e.NewValue != null)
            {
                var coll = (ObservableCollection<Model.Figure>)e.NewValue;
                coll.CollectionChanged += action;
            }
        }

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (button1.Content.ToString() == "Edit" && (this.DataContext as MainViewModel).SelectedFigure != null)
            {
                button1.Content = "Save";

            }
            else if (button1.Content.ToString() == "Save")
            {
                button1.Content = "Edit";
            }
        }
    }
}
