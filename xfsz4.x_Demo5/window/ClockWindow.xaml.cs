using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace xfsz4.x_Demo5.window
{
    /// <summary>
    /// ClockWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClockWindow : Window
    {
        public ClockWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            borderset();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            log.LogW.NewInfoLog("已加载时钟程序");
            borderset();
            kfruit();
        }
        async void borderset()
        {
            while (true)
            {
                if(Pub.End==true)
                {
                    this.Close();
                }
                if(Pub.ClockWindow_Top==true)
                {
                    this.Topmost = true;
                }
                else
                {
                    this.Topmost = false;
                }
                Topmost = Pub.ClockWindow_Top;
                scax.ScaleX = Pub.Zoom;
                scax.ScaleY = Pub.Zoom;
                viewtime.Foreground = Pub.TimeColor;
                viewdate.Foreground = Pub.DateColor;
                viewb.Background = Pub.CloseBGColor;
                viewb.BorderBrush = Pub.CloseBorder;
                //viewborder.Background = Pub.BG;
                viewtime.Text = DateTime.Now.ToString(Pub.Format[1]);
                viewdate.Content = DateTime.Now.ToString(Pub.Format[0]);
                viewborder.CornerRadius = new(Pub.Border[0], Pub.Border[1], Pub.Border[2], Pub.Border[3]);
                await Task.Delay(50);
            }
        }

        private void viewborder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            log.LogW.NewInfoLog("移动窗口");
            this.DragMove();
        }

        private void viewtime_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            log.LogW.NewInfoLog("移动窗口");
            this.DragMove();
        }

        private void viewdate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            log.LogW.NewInfoLog("移动窗口");
            this.DragMove();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Pub.CloseMode == true)
            {
                Pub.End = true;
            }
            else
            {
                this.Visibility = Visibility.Hidden;
                Pub.CloseTime = DateTime.Now;
                Pub.Closing = true;
                Hold();
            }
        }
        async void Hold ()
        {
            await Task.Delay(Pub.Close * 1000);
            this.Visibility = Visibility.Visible;
            Pub.Closing = false;
        }
        async void Holdw()
        {
            await Task.Delay(Pub.Close * 1000);
            this.Visibility = Visibility.Visible;
            Pub.Closing = false;
            Pub.kf = false;
        }
        async void kfruit()
        {
            while(true)
            {
                if (Pub.kf == true)
                {
                    if(Pub.kf1 == false)
                    {
                        this.Visibility = Visibility.Hidden;
                        Pub.CloseTime = DateTime.Now;
                        Pub.Closing = true;
                        Holdw();
                        Pub.kf1 = true;
                    }
                }
                else
                {
                    Pub.kf1 = false;
                }
                await Task.Delay(20);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Pub.kf1 = false;
            return;
        }
    }

}
