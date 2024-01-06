using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using xfsz_xml;
using xfsz4.x_Demo5.window;

namespace xfsz4.x_Demo5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Pub.End = true;
            this.Close();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = !Pub.MainWindow_Top;
            Pub.MainWindow_Top = !Pub.MainWindow_Top;

        }

        private void topleft_ValueChanged(object sender, HandyControl.Data.FunctionEventArgs<double> e)
        {

        }
        async void borderset()
        {
            while (true)
            {
                stat_format.Text = "格式预览 " + DateTime.Now.ToString(Pub.Format[0]) + " ; " + DateTime.Now.ToString(Pub.Format[1]);
                viewtime.Text = DateTime.Now.ToString(Pub.Format[1]);
                viewdate.Content = DateTime.Now.ToString(Pub.Format[0]);
                viewborder.CornerRadius = new(topleft.Value, topright.Value, bottonright.Value, bottonleft.Value);
                bor.CornerRadius = new(topleft.Value, topright.Value, bottonright.Value, bottonleft.Value);
                Pub.Border[0] = topleft.Value;
                Pub.Border[1] = topright.Value;
                Pub.Border[2] = bottonright.Value;
                Pub.Border[3] = bottonleft.Value;
                await Task.Delay(50);
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            borderset();
            //初始化
            //主窗口置顶
            String mwt = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/Window", "Top");
            Pub.MainWindow_Top = Convert.ToBoolean(mwt);
            windowtop.IsChecked = Pub.MainWindow_Top;
            this.Topmost = Pub.MainWindow_Top;
            //标签显示
            String tag = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/App", "Tag");
            bool tagc = Convert.ToBoolean(tag);
            if (tagc == false)
            {
                Tag.Visibility = Visibility.Hidden;
            }
            else
            {
                Tag.Visibility = Visibility.Visible;
            }
            String cwt = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/App", "Tag");
            bool cwtc = Convert.ToBoolean(cwt);
            String holdw = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/ClockWIndow", "CloseWindowMode");
            if (holdw == "hold")
            {
                Pub.CloseMode = false;
            }
            else if(holdw == "false")
            {
                Pub.CloseMode = true;
            }
            String holdt = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/ClockWIndow", "HoldTime");
            Pub.Close = Convert.ToInt32(holdt);
            Hold();
            //Color();
        }

        private void ColorPicker_SelectedColorChanged(object sender, HandyControl.Data.FunctionEventArgs<Color> e)
        {

        }

        private void dateformat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Pub.Format[0] = dateformat.Text;
            }
        }

        private void timeformat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Pub.Format[1] = timeformat.Text;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var Opena = FindResource("a_home_start") as Storyboard;
            Opena.Begin();
        }

        private void TabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TabItem_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            About abo = new();
            abo.ShowDialog();
            
        }

        private void tool_run_Click(object sender, RoutedEventArgs e)
        {
            tool_run.IsEnabled = false;
            stat_run.Text = "运行中";
            Pub.StartTime = DateTime.Now;
            Pub.End = false;
            RunTime();
            ClockWindow cw = new();
            cw.Show();
        }
        async void RunTime()
        {
            while (true)
            {
                if(Pub.End == false)
                {
                    TimeSpan run = DateTime.Now - Pub.StartTime;
                    Pub.EndTime = DateTime.Now - Pub.StartTime;
                    await Task.Delay(100);
                    stat_run.Text = "已运行" + run.ToString(@"dd\天hh\:mm\:ss");
                }
                else
                {
                    tool_run.IsEnabled = true;
                    await Task.Delay(100);
                    stat_run.Text = "上次运行" + Pub.EndTime.ToString(@"dd\天hh\:mm\:ss");
                }
            }
        }

        private void Tool_close_Click(object sender, RoutedEventArgs e)
        {
            Pub.End = true;
            tool_run.IsEnabled = true;
        }

        private void window_Closed(object sender, EventArgs e)
        {
            Pub.End = true;
        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            Pub.ClockWindow_Top = (bool)checkbox_clocktop.IsChecked;
        }

        private void numericUpDown_ValueChanged(object sender, HandyControl.Data.FunctionEventArgs<double> e)
        {
            Pub.Close = Convert.ToInt16(numericUpDown.Value);
        }
        async void Hold ()
        {
            while (true)
            {
                if (Pub.Closing == true)
                {
                    Pub.OpenTime = DateTime.Now - Pub.CloseTime;
                    double ms = Pub.OpenTime.TotalMilliseconds;
                    DateTime ts = Pub.CloseTime.AddSeconds(Pub.Close);
                    TimeSpan d = ts - Pub.CloseTime;
                    stat_closeprog.Maximum = d.TotalMilliseconds;
                    Trace.WriteLine(d.TotalMilliseconds);
                    stat_closeprog.Value = Pub.OpenTime.TotalMilliseconds;
                    stat_close.Text = d.TotalSeconds + "秒后打开";
                }
                else
                {
                    stat_close.Text = "未关闭";
                }
                await Task.Delay(20);
            }
        }

        private void Tool_vclock_Click(object sender, RoutedEventArgs e)
        {
            Pub.kf = true;
        }

        private void tool_clock_Click(object sender, RoutedEventArgs e)
        {

        }

        private void checkBox_Click_2(object sender, RoutedEventArgs e)
        {
            Pub.CloseMode = !Pub.CloseMode;
        }
        async void Color()
        {
            while (true)
            {
                if(Colorc.SelectedIndex == 0)//时间字体
                {
                    viewtime.Foreground = ColorPicker.SelectedBrush;
                }
                else if (Colorc.SelectedIndex == 1)//日期字体
                {

                }
                else if (Colorc.SelectedIndex == 2)//关闭按钮背景
                {

                }
                else if (Colorc.SelectedIndex == 3)//关闭按钮边框
                {

                }
                else if (Colorc.SelectedIndex == 4)//纯色背景
                {

                }
                else if (Colorc.SelectedIndex == 5)//渐变背景起点
                {

                }
                else if (Colorc.SelectedIndex == 6)//渐变背景终点
                {

                }
                await Task.Delay(50);
            }
        }

        private void ColorPicker_Canceled(object sender, EventArgs e)
        {

        }

        private void ColorPicker_SelectedColorChanged1(object sender, HandyControl.Data.FunctionEventArgs<Color> e)
        {
            int s = 0;
            try
            {
                if(Colorc == null)
                {
                    s = 0;
                }
                else
                {
                    s = Colorc.SelectedIndex;
                }
            }
            catch
            {
                s = 0;
            }
            finally
            {
                if (s == 0)//时间字体
                {
                    if (!(viewtime == null))
                    {
                        viewtime.Foreground = ColorPicker.SelectedBrush;
                    }
                }
                else if (s == 1)//日期字体
                {

                }
                else if (s == 2)//关闭按钮背景
                {

                }
                else if (s == 3)//关闭按钮边框
                {

                }
                else if (s == 4)//纯色背景
                {

                }
                else if (s == 5)//渐变背景起点
                {

                }
                else if (s == 6)//渐变背景终点
                {

                }
            }
        }
    }
}