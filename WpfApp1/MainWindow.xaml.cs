using LiteEntity;
using LiteEntity.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfBase.Mvvm;
using Util;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(() => G1.Children.Clear());
        }


        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            Point position1 = b.PointToScreen(new Point(0d, 0d));
            Point point1 = new Point(position1.X + 40, position1.Y + 16);

            Point position2 = B1.PointToScreen(new Point(0d, 0d));
            Point point2 = new Point(position2.X + 50, position2.Y + 26);

            Line line = new Line
            {
                X1 = point1.X,
                Y1 = point1.Y,
                X2 = point2.X,
                Y2 = point2.Y,
                Stroke = Brushes.DarkGray,
            };

            G1.Children.Add(line);
        }
    }
}
