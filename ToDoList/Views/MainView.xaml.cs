using Prism.Events;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using ToDoList.Models;
using ToDoList.Services.EventType;
using System.Collections.Generic;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPTStr)] string lpClassName, [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName);

        [DllImport("user32")]
        private static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public MainView(IEventAggregator ea)
        {
            InitializeComponent();
            Things = new List<Thing>();
            //初始化显示位置
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = 0;
            //接受提醒通知
            _eventAggregator = ea;
            ea.GetEvent<MainViewNotify>().Subscribe(Notify, ThreadOption.UIThread);
            //.net5 自动生成
            IntPtr pWnd = FindWindow("Progman", null);
            pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);
            IntPtr tWnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            SetParent(tWnd, pWnd);
        }

        private void NotifyIcon_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            NotifyMedia.Stop();
            if (Things.Count!=0)
            {
                Thing thing = Things[0]; 
                EditView editView = new EditView();
                editView.Show();
                _eventAggregator.GetEvent<EditViewTransmit>().Publish(thing);
                Things.RemoveAt(0);
            }
            else
            {
                EditView editView = new EditView();
                editView.Show();
                _eventAggregator.GetEvent<EditViewTransmit>().Publish(null);
            }
            if (Things.Count==0)
            {
                NotifyIcon.IsBlink = false;
            }
            
        }

        #region 属性定义

        public List<Thing> Things { get; set; }
        private IEventAggregator _eventAggregator;

        #endregion 属性定义

        #region 方法

        private void Notify(Thing thing)
        {
            try
            {
                NotifyIcon.IsBlink = true;
                Things.Add(thing);
                
                NotifyMedia.Play();
            }
            catch (Exception )
            {
                throw;
            }
        }

        #endregion 方法

        private void NotifyIcon_Click(object sender, RoutedEventArgs e)
        {
            this.Activate();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NotifyMedia.Play();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}