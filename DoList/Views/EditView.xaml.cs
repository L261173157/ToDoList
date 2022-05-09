using System.Windows;

namespace DoList.Views;

/// <summary>
///     Interaction logic for PrismWindow1.xaml
/// </summary>
public partial class EditView : Window
{
    public EditView()
    {
        InitializeComponent();
        tbContent.Focus();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}