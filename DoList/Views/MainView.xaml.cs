using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Database.Models.DoList;
using DoList.Services.EventType;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;

namespace DoList.Views;

/// <summary>
///     Interaction logic for MainView.xaml
/// </summary>
public partial class MainView : UserControl
{
    private IContainerExtension container;
    private IRegionManager regionManager;

    public MainView(IEventAggregator ea, IRegionManager regionManager, IContainerExtension container)
    {
        InitializeComponent();
        this.regionManager = regionManager;
        this.container = container;
        Things = new List<Thing>();

        //接受提醒通知
        _eventAggregator = ea;
        ea.GetEvent<MainViewNotify>().Subscribe(Notify, ThreadOption.UIThread);
    }


    private void NotifyIcon_MouseDoubleClick(object sender, RoutedEventArgs e)
    {
        // NotifyMedia.Stop();
        if (Things.Count != 0)
        {
            var thing = Things[0];
            var editView = new EditView();
            editView.Show();
            _eventAggregator.GetEvent<EditViewTransmit>().Publish(thing);
            Things.RemoveAt(0);
        }
        else
        {
            var editView = new EditView();
            editView.Show();
            _eventAggregator.GetEvent<EditViewTransmit>().Publish(null);
        }

        if (Things.Count == 0) NotifyIcon.IsBlink = false;
    }

    #region 方法

    private void Notify(Thing thing)
    {
        NotifyIcon.IsBlink = true;
        Things.Add(thing);

        // NotifyMedia.Play();
    }

    #endregion 方法


    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
    }

    #region 属性定义

    public List<Thing> Things { get; set; }
    private readonly IEventAggregator _eventAggregator;

    #endregion 属性定义
}