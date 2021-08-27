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
using DoList.Models;
using DoList.Services.EventType;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;

namespace DoList.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        private IRegionManager regionManager;
        private IContainerExtension container;

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

            if (Things.Count == 0)
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

                // NotifyMedia.Play();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion 方法

        private void btnShowDone_Click(object sender, RoutedEventArgs e)
        {
            switch (btnShowDone.Content)
            {
                case "已完成":
                    btnShowDone.Content = "全部";
                    break;
                case "全部":
                    btnShowDone.Content = "已完成";
                    break;
                default:
                    btnShowDone.Content = "已完成";
                    break;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btnShowDone.Content = "已完成";
        }
    }
}