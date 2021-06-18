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
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int cameraIndex = 0;
            UsbCamera.VideoFormat[] formats = UsbCamera.GetVideoFormat(cameraIndex);
            for (int i = 0; i < formats.Length; i++) Console.WriteLine("{0}:{1}", i, formats[i]);

            UsbCamera camera = new UsbCamera(cameraIndex, formats[0]);
            camera.Start();

            var dispatcherTimer = new DispatcherTimer() ;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(10);
            dispatcherTimer.Tick += (s, ev) => X1.Source = camera.GetBitmap();

            dispatcherTimer.Start();

        }
    }
}
