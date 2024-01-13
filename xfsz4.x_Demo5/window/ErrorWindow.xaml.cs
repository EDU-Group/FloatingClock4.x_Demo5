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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace xfsz4.x_Demo5.window
{
    /// <summary>
    /// ErrorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Err.Text = Pub.ErrorInfo;
            Pub.Error = true;
            Error();
        }

        private async void Error()
        {
            log.LogW.NewInfoLog("播放动画:a_Err");
            var Opena = FindResource("a_Err") as Storyboard; Opena.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Pub.Error = false;
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            log log = new();
            log.LogW.WindowState = WindowState.Normal;
            log.LogW.Visibility = Visibility.Visible;
            log.LogW.Show();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
