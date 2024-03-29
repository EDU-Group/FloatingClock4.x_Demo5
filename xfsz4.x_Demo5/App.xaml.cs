﻿using System.Configuration;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using xfsz4.x_Demo5.window;

namespace xfsz4.x_Demo5
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = null;
        const string appName = "xfsz4.x_Demo5";
        bool createdNew;
        ErrorWindow errorWindow = new();

        protected override void OnStartup(StartupEventArgs e)
        {
            _mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                //应用程序已经在运行！当前的执行退出。
                Application.Current.Shutdown();
            }

            base.OnStartup(e);
        }

        public void Restart()
        {
            // 释放互斥锁
            _mutex.ReleaseMutex();

            // 重启应用程序
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        public App()
        {
            //首先注册开始和退出事件
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_Exit);
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            //UI线程未捕获异常处理事件
            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            //程序退出时需要处理的业务
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                log.LogW.NewStopLog("在UI线程上引发的异常，异常已被处理:" + e.Exception.Message);
                log.LogW.NewErrorLog("继续运行可能造成一些未知问题,建议重新启动悬浮时钟");
                e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出      
                Pub.ErrorInfo = "发生了错误:\nMessage: " + e.Exception.Message + "\nErrorCode: " + e.Exception.HResult;
                ErrorWindow errw = new();
                errw.Show();

            }
            catch (Exception ex)
            {
                log.LogW.NewStopLog("在UI线程上引发无法处理的异常" + e.Exception.Message);
                //此时程序出现严重异常，将强制结束退出
                Pub.ErrorInfo = "发生了错误:\nMessage: " + e.Exception.Message + "\nErrorCode: " + e.Exception.HResult;
                ErrorWindow errw = new();
                errw.Show();
            }

        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sbEx = new StringBuilder();
            if (e.IsTerminating)
            {
                sbEx.Append("非UI线程发生致命错误");
            }
            sbEx.Append("非UI线程异常：");
            if (e.ExceptionObject is Exception)
            {
                sbEx.Append(((Exception)e.ExceptionObject).Message);
            }
            else
            {
                sbEx.Append(e.ExceptionObject);
            }
            Pub.ErrorInfo = "发生了错误:\nMessage: " + sbEx.ToString() + "\nErrorCode: " + sbEx;
            ErrorWindow errw = new();
            errw.Show();
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //task线程内未处理捕获
            MessageBox.Show("Task线程异常：" + e.Exception.Message, "出错了", MessageBoxButton.OK, MessageBoxImage.Error);
            e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
        }
    }

}
