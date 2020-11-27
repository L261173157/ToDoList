using System.Windows;
using ToDoList.Models;
using System;



namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for RemindView.xaml
    /// </summary>
    public partial class RemindView : Window
    {
        public RemindView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       


    }
}
