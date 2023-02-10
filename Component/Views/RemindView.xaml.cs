using System.Windows;
using System.Windows.Controls;

namespace Component.Views;

/// <summary>
///     Interaction logic for WeatherView
/// </summary>
public partial class RemindView : UserControl
{
    public RemindView()
    {
        InitializeComponent();
    }

    private void tbCity_GotFocus(object sender, RoutedEventArgs e)
    {
        tbCity.Text = "";
    }
}