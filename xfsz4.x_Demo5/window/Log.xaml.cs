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
using System.Windows.Shapes;

namespace xfsz4.x_Demo5.window
{
    /// <summary>
    /// Log.xaml 的交互逻辑
    /// </summary>
    public partial class Log : Window
    {
        public Log()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        internal async void NewInfoLog(string Log)
        {
            Run r = new Run(DateTime.Now.ToString("(HH:mm:ss)") + "[Info]" + Log);
            Paragraph para = new Paragraph();
            para.Inlines.Add(r);
            para.Background = info.Background;
            para.Foreground = info.Foreground;
            para.FontSize = info.FontSize;
            para.BorderBrush = info.BorderBrush;
            para.BorderThickness = info.BorderThickness;
            rtb.Document.Blocks.Add(para);
            rtb.ScrollToEnd();
        }
        internal async void NewWarnLog(string Log)
        {
            Run r = new Run(DateTime.Now.ToString("(HH:mm:ss)") + "[Warn]" + Log);
            Paragraph para = new Paragraph();
            para.Inlines.Add(r);
            para.Background = warn.Background;
            para.Foreground = warn.Foreground;
            para.FontSize = warn.FontSize;
            para.BorderBrush = warn.BorderBrush;
            para.BorderThickness = warn.BorderThickness;
            rtb.Document.Blocks.Add(para);
            rtb.ScrollToEnd();
        }
        internal async void NewErrorLog(string Log)
        {
            Run r = new Run(DateTime.Now.ToString("(HH:mm:ss)") + "[Error]" + Log);
            Paragraph para = new Paragraph();
            para.Inlines.Add(r);
            para.Background = error.Background;
            para.FontSize = error.FontSize;
            para.Foreground = error.Foreground;
            para.BorderBrush = error.BorderBrush;
            para.BorderThickness = error.BorderThickness;
            rtb.Document.Blocks.Add(para);
            rtb.ScrollToEnd();
        }
        internal async void NewStopLog(string Log)
        {
            Run r = new Run(DateTime.Now.ToString("(HH:mm:ss)") + "[Stop]" + Log);
            Paragraph para = new Paragraph();
            para.Inlines.Add(r);
            para.Background = stop.Background;
            para.Foreground = stop.Foreground;
            para.FontSize = stop.FontSize;
            para.BorderBrush = stop.BorderBrush;
            para.BorderThickness = stop.BorderThickness;
            rtb.Document.Blocks.Add(para);
            rtb.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
