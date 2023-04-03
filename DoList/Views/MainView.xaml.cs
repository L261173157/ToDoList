using System.Windows;
using System.Windows.Controls;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;

namespace DoList.Views;

/// <summary>
///     Interaction logic for MainView.xaml
/// </summary>
public partial class MainView : UserControl
{
    #region 属性定义

    private readonly IEventAggregator _eventAggregator;

    #endregion 属性定义

    private IContainerExtension container;
    private IRegionManager regionManager;

    public MainView(IEventAggregator ea, IRegionManager regionManager, IContainerExtension container)
    {
        InitializeComponent();
        this.regionManager = regionManager;
        this.container = container;
        _eventAggregator = ea;
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
    }
}