﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xfsz_xml;

namespace xfsz4.x_Demo5
{
    internal class Pub
    {
        public static Color DateColor;
        public static Color TimeColor;
        public static Color BGColorS;
        public static Color BGColorE;
        public static Color CloseBGColor;
        public static Color CloseBorder;
        public static bool MainWindow_Top = false;
        public static bool ClockWindow_Top = false;
        public static DateTime StartTime;
        public static bool End = false;
        public static TimeSpan EndTime;
        public static int Close = 300;
        public static bool Closing = false;
        public static DateTime CloseTime;
        public static TimeSpan OpenTime;
        public static bool kf = false;
        public static bool kf1 = false;
        public static bool CloseMode = false; //f定时打开t直接关
        public static double[] Border = { 16, 16, 16, 16 }; 
        public static string Pach = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string[] Format = { "yyyy/MM/dd dddd", "hh:mm:ss" };
    }
}