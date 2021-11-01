using System.Windows.Controls;

namespace Component.Views
{
    /// <summary>
    /// Interaction logic for WeatherView
    /// </summary>
    public partial class WeatherView : UserControl
    {
        public WeatherView()
        {
            InitializeComponent();
        }

        private void tbCity_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            tbCity.Text = "";
        }
    }
}
