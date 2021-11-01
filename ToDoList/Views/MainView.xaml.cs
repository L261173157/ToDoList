using Prism.Events;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Collections.Generic;

using DoList.Services.EventType;
using DoList.Views;
using Prism.Ioc;
using Prism.Regions;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPTStr)] string lpClassName,
            [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName);

        [DllImport("user32")]
        private static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private IRegionManager regionManager;
        private IContainerExtension container;

        public MainView(IEventAggregator ea, IRegionManager regionManager, IContainerExtension container)
        {
            InitializeComponent();
            this.regionManager = regionManager;
            this.container = container;

            //初始化显示位置
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = 0;
            //接受提醒通知
            _eventAggregator = ea;

            //.net5 自动生成
            IntPtr pWnd = FindWindow("Progman", null);
            pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);
            IntPtr tWnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            SetParent(tWnd, pWnd);
        }


        #region 属性定义

        private IEventAggregator _eventAggregator;

        #endregion 属性定义

        #region 方法

        #endregion 方法

        private void NotifyIcon_Click(object sender, RoutedEventArgs e)
        {
            this.Activate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            regionManager.RequestNavigate("ComponentRegion", "WeatherView");
            regionManager.RequestNavigate("ToDoListRegion", "MainView");
        }

        private void BtnTranslate_Click(object sender, RoutedEventArgs e)
        {
            regionManager.RequestNavigate("ComponentRegion", "TranslateView");
        }

        private void BtnWeather_Click(object sender, RoutedEventArgs e)
        {
            regionManager.RequestNavigate("ComponentRegion", "WeatherView");
        }
    }
}