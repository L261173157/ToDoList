using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToDoList.Views
{
    /// <summary>
    /// NewThingView.xaml 的交互逻辑
    /// </summary>
    public partial class NewThingView : Window
    {
        public NewThingView()
        {
            InitializeComponent();
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       
    }
}
