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
    public class log
    {
        public static Log LogW = new();
    }
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
                viewborder.Background = (Brush)Application.Current.TryFindResource(Pub.CTheme);
                await Task.Delay(50);
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            log.LogW.NewWarnLog("!!!-= 开始初始化 =-!!!");
            DateTime start = DateTime.Now;
            log.LogW.Show();
            log.LogW.NewInfoLog("准备启动");
            borderset();
            log.LogW.NewInfoLog("启动预览窗口");
            //初始化
            //主窗口置顶
            log.LogW.NewInfoLog("读Xml特定节点的属性top");
            String mwt = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/Window", "Top");
            log.LogW.NewInfoLog("节点的属性top为" + mwt);
            Pub.MainWindow_Top = Convert.ToBoolean(mwt);
            windowtop.IsChecked = Pub.MainWindow_Top;
            log.LogW.NewInfoLog("设置主窗口置顶:" + mwt);
            this.Topmost = Pub.MainWindow_Top;
            //标签显示
            log.LogW.NewInfoLog("读Xml特定节点的属性tag");
            String tag = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/App", "Tag");
            bool tagc = Convert.ToBoolean(tag);
            if (tagc == false)
            {
                log.LogW.NewInfoLog("隐藏tag");
                Tag.Visibility = Visibility.Hidden;
            }
            else
            {
                log.LogW.NewInfoLog("显示tag");
                Tag.Visibility = Visibility.Visible;
            }
            String cwt = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/App", "Tag");
            bool cwtc = Convert.ToBoolean(cwt);
            log.LogW.NewInfoLog("读Xml特定节点的属性CloseWindowMode");
            String holdw = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/ClockWIndow", "CloseWindowMode");
            if (holdw == "hold")
            {
                    log.LogW.NewInfoLog("时钟关闭模式:等待");
                Pub.CloseMode = false;
            }
            else if(holdw == "false")
            {
                log.LogW.NewInfoLog("时钟关闭模式:退出");
                Pub.CloseMode = true;
            }
            log.LogW.NewInfoLog("读Xml特定节点的属性HoldTime");
            String holdt = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/ClockWIndow", "HoldTime");
            Pub.Close = Convert.ToInt32(holdt);
            log.LogW.NewInfoLog("时钟等待时间:"+holdt+"秒");
            Hold();
            log.LogW.NewInfoLog("启动时钟关闭侦测");
            //Color();
            DateTime end = DateTime.Now;
            TimeSpan time = end - start;
            log.LogW.NewInfoLog("读Xml特定节点的属性Top");
            String ct = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/ClockWIndow", "Top");
            Pub.ClockWindow_Top = Convert.ToBoolean(ct);
            log.LogW.NewInfoLog("时钟默认置顶:"+ct);
            log.LogW.NewInfoLog("初始化默认颜色");
            Pub.TimeColor = viewtime.Foreground;
            Pub.DateColor = viewdate.Foreground;
            Pub.CloseBGColor = viewb.Background;
            Pub.CloseBorder = viewb.BorderBrush;
            log.LogW.NewInfoLog("读Xml特定节点的属性WindowZoom");
            String czm = xfsz_xml.Xml.XmlReadNodeAttributeValue(Pub.Pach + @"\" + @"Data\xfsz_Data.xml", @"Data/ClockWIndow", "WindowZoom");
            log.LogW.NewInfoLog("初始化缩放倍数");
            Pub.Zoom = Convert.ToInt32(czm);
            log.LogW.NewWarnLog("初始化结束,用时"+time.TotalMilliseconds+"ms");

        }

        private void ColorPicker_SelectedColorChanged(object sender, HandyControl.Data.FunctionEventArgs<Color> e)
        {

        }

        private void dateformat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Pub.Format[0] = dateformat.Text;
                log.LogW.NewInfoLog("触发日期格式的更改:"+dateformat.Text);
            }
        }

        private void timeformat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Pub.Format[1] = timeformat.Text;
                log.LogW.NewInfoLog("触发时间格式的更改:" + timeformat.Text);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            log.LogW.NewWarnLog("生成的UUID:" + Guid.NewGuid().ToString());
            log.LogW.NewInfoLog("播放动画:a_home_start");
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
            log.LogW.NewInfoLog("打开关于页面");
            About abo = new();
            abo.ShowDialog();
        }

        private void tool_run_Click(object sender, RoutedEventArgs e)
        {
            DateTime open = DateTime.Now;
            log.LogW.NewInfoLog("正在启动时钟");
            tool_run.IsEnabled = false;
            stat_run.Text = "运行中";
            Pub.StartTime = DateTime.Now;
            Pub.End = false;
            RunTime();
            ClockWindow cw = new();
            cw.Show();
            DateTime opene = DateTime.Now;
            TimeSpan Time = opene - open;
            log.LogW.NewWarnLog("启动成功,用时" + Time.TotalMilliseconds.ToString() + "ms");
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
            log.LogW.NewInfoLog("正在关闭时钟");
            Pub.End = true;
            tool_run.IsEnabled = true;
        }

        private void window_Closed(object sender, EventArgs e)
        {
            Pub.End = true;
            
        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            log.LogW.NewInfoLog("设置时钟置顶:"+ Convert.ToString(checkbox_clocktop.IsChecked));
            Pub.ClockWindow_Top = (bool)checkbox_clocktop.IsChecked;
        }

        private void numericUpDown_ValueChanged(object sender, HandyControl.Data.FunctionEventArgs<double> e)
        {
            if(numericUpDown.Value > 32767)
            {
                log.LogW.NewErrorLog("等待时间设置值超出Int16整数最大范围(+32767)");
                if (MessageBox.Show("设置值超出Int16整数最大范围(+32767)\n将输入值" + Convert.ToInt64(numericUpDown.Value) + "改为32767\n按下是强制修改","悬浮时钟",MessageBoxButton.YesNo,MessageBoxImage.Error,MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    Pub.Close = Convert.ToInt16(numericUpDown.Value);
                }
                else
                {
                    log.LogW.NewWarnLog("已更改为32767");
                    Pub.Close = 32767;
                    numericUpDown.Value = 32767;
                }

            }
            else
            {
                log.LogW.NewInfoLog("等待时间设置为" + Convert.ToInt16(numericUpDown.Value));
                Pub.Close = Convert.ToInt16(numericUpDown.Value);
            }
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
            log.LogW.NewInfoLog("设置关闭模式");
            Pub.CloseMode = !Pub.CloseMode;
        }
        async void Color()
        {
            while (true)
            {
                if(Colorc.SelectedIndex == 0)//时间字体
                {
                    viewtime.Foreground = ColorPicker.SelectedBrush;
                    Pub.TimeColor = viewtime.Foreground;
                }
                else if (Colorc.SelectedIndex == 1)//日期字体
                {
                    viewdate.Foreground = ColorPicker.SelectedBrush;
                    Pub.DateColor = viewdate.Foreground;
                }
                else if (Colorc.SelectedIndex == 2)//关闭按钮背景
                {
                    viewb.Background = ColorPicker.SelectedBrush;
                    Pub.CloseBGColor = viewb.Background;
                }
                else if (Colorc.SelectedIndex == 3)//关闭按钮边框
                {
                    viewb.BorderBrush = ColorPicker.SelectedBrush;
                    Pub.CloseBorder = viewb.BorderBrush;
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
                if(Pub.ColorPicker == false)
                {
                    if (s == 0)//时间字体
                    {
                        if (!(viewtime == null))
                        {
                            viewtime.Foreground = ColorPicker.SelectedBrush;
                            Pub.TimeColor = viewtime.Foreground;
                        }
                    }
                    else if (s == 1)//日期字体
                    {
                        if (!(viewdate == null))
                        {
                            viewdate.Foreground = ColorPicker.SelectedBrush;
                            Pub.DateColor = viewdate.Foreground;
                        }
                    }
                    else if (s == 2)//关闭按钮背景
                    {
                        if (!(viewb == null))
                        {
                            viewb.Background = ColorPicker.SelectedBrush;
                            Pub.CloseBGColor = viewb.Background;
                        }
                    }
                    else if (s == 3)//关闭按钮边框
                    {
                        if (!(viewb == null))
                        {
                            viewb.BorderBrush = ColorPicker.SelectedBrush;
                            Pub.CloseBorder = viewb.BorderBrush;
                        }
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

        private void Colorc_Selected(object sender, RoutedEventArgs e)
        {
            int s = Colorc.SelectedIndex;
            if (s == 0)//时间字体
            {
                if (!(viewtime == null))
                {
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

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            Pub.ColorPicker = true;
            //初始化时也会触发此事件，此时ColorPicker和Viewtime未加载，导致崩溃
            if (!(ColorPicker == null || viewtime == null)) 
            {
                log.LogW.NewInfoLog("时间文本颜色为：" + viewtime.Foreground.ToString());
                ColorPicker.SelectedBrush = (SolidColorBrush)viewtime.Foreground;
            }
            Pub.ColorPicker = false;
        }

        private void CB_DateColor(object sender, RoutedEventArgs e)
        {
            Pub.ColorPicker = true;
            if (!(ColorPicker == null || viewdate == null))
            {
                log.LogW.NewInfoLog("日期文本颜色为：" + viewdate.Foreground.ToString());
                ColorPicker.SelectedBrush = (SolidColorBrush)viewdate.Foreground;
            }
            Pub.ColorPicker = false;
        }

        private void CB_ButtonBG(object sender, RoutedEventArgs e)
        {
            Pub.ColorPicker = true;
            if (!(ColorPicker == null || viewb == null))
            {
                log.LogW.NewInfoLog("按钮背景颜色为：" + viewb.Background.ToString());
                ColorPicker.SelectedBrush = (SolidColorBrush)viewb.Background;
            }
            Pub.ColorPicker = false;
        }

        private void CB_ButtonBorder(object sender, RoutedEventArgs e)
        {
            Pub.ColorPicker = true;
            if (!(ColorPicker == null || viewb == null))
            {
                log.LogW.NewInfoLog("按钮边框颜色为：" + viewb.BorderBrush.ToString());
                ColorPicker.SelectedBrush = (SolidColorBrush)viewb.BorderBrush;
            }
            Pub.ColorPicker = false;
        }

        private void CB_BG(object sender, RoutedEventArgs e)
        {
            Pub.ColorPicker = true;
            if (!(ColorPicker == null || viewborder == null))
            {
                log.LogW.NewWarnLog("已知的错误,此功能未完善，目前无法使用");
                ColorPicker.SelectedBrush = (SolidColorBrush)viewborder.Background;
            }
            Pub.ColorPicker = false;
        }

        private void CB_BGstart(object sender, RoutedEventArgs e)
        {
            Pub.ColorPicker = true;
            if (!(ColorPicker == null || viewborder == null))
            {

            }
            Pub.ColorPicker = false;
        }

        private void CB_BGend(object sender, RoutedEventArgs e)
        {
            
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Pub.Zoom = slider.Value;
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void summer_Selected(object sender, RoutedEventArgs e)
        {
            Pub.CTheme = "summer";
            log.LogW.NewInfoLog("外观库更改:" + Pub.CTheme);
        }

        private void yellowpink_Selected(object sender, RoutedEventArgs e)
        {
            Pub.CTheme = "yellowpink";
            log.LogW.NewInfoLog("外观库更改:" + Pub.CTheme);
        }

        private void sport_Selected(object sender, RoutedEventArgs e)
        {
            Pub.CTheme = "sport";
            log.LogW.NewInfoLog("外观库更改:" + Pub.CTheme);
        }

        private void hotandcold_Selected(object sender, RoutedEventArgs e)
        {
            Pub.CTheme = "hotandcold";
            log.LogW.NewInfoLog("外观库更改:" + Pub.CTheme);
        }

        private void ice_Selected(object sender, RoutedEventArgs e)
        {
            Pub.CTheme = "ice";
            log.LogW.NewInfoLog("外观库更改:" + Pub.CTheme);
        }

        private void sun_Selected(object sender, RoutedEventArgs e)
        {
            Pub.CTheme = "sun";
            log.LogW.NewInfoLog("外观库更改:" + Pub.CTheme);
        }

        private void magic_Selected(object sender, RoutedEventArgs e)
        {
            Pub.CTheme = "magic";
            log.LogW.NewInfoLog("外观库更改:" + Pub.CTheme);
        }

        private void genshin_Selected(object sender, RoutedEventArgs e)
        {
            Pub.CTheme = "genshin";
            log.LogW.NewInfoLog("外观库更改:" + Pub.CTheme);
        }

        private void gs01_Selected(object sender, RoutedEventArgs e)
        {
            Pub.CTheme = "gskkkkk";
            log.LogW.NewInfoLog("外观库更改:" + Pub.CTheme);
        }

        private void checkbox_clocktopppp_Click(object sender, RoutedEventArgs e)
        {
            Pub.ClockWindow_Topttt = !Pub.ClockWindow_Topttt;
        }
    }
    public class Exit
    {
        public void MainExit()
        {
            //Close();
        }
    }
}