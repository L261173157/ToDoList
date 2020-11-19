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


using System.ComponentModel;
using System.Data;
using System.Drawing;


using System.Runtime.InteropServices;


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
        public MainView()
        {
            InitializeComponent();
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = 0;

            IntPtr pWnd = FindWindow("Progman", null);
            pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SHELLDLL_DefVIew", null);
            pWnd = FindWindowEx(pWnd, IntPtr.Zero, "SysListView32", null);
            IntPtr tWnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            SetParent(tWnd, pWnd);
        }

       
    }
}
