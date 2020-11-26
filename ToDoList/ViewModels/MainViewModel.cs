using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;
using ToDoList.Db;
using ToDoList.Models;
using ToDoList.Services.EventType;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IEventAggregator ea)

        {
            ea.GetEvent<MainViewRefresh>().Subscribe(Refresh);

            Things = new ObservableCollection<Thing>();
            _eventAggregator = ea;
            Refresh();
            SetTimer();
        }

        #region 属性定义

        private static Timer timer;

        private IEventAggregator _eventAggregator;

        private readonly ThingsContext db = new ThingsContext();
        public ObservableCollection<Thing> Things { get; set; }

        private Visibility mainViewVisiblity = Visibility.Visible;

        public Visibility MainViewVisiblity
        {
            get { return mainViewVisiblity; }
            set { SetProperty(ref mainViewVisiblity, value); }
        }

        #endregion 属性定义

        #region 命令方法

        private DelegateCommand _newThingCmd;

        //弹出新建事务窗体
        public DelegateCommand NewThingViewCmd =>
            _newThingCmd ?? (_newThingCmd = new DelegateCommand(ExecuteNewThingViewCmd));

        private void ExecuteNewThingViewCmd()
        {
            NewThingView newThingView = new NewThingView();
            newThingView.Show();
        }

        private DelegateCommand _testCmd;

        public DelegateCommand TestCmd =>
            _testCmd ?? (_testCmd = new DelegateCommand(Test));

        private DelegateCommand _RefreshCmd;

        public DelegateCommand RefreshCmd =>
            _RefreshCmd ?? (_RefreshCmd = new DelegateCommand(Refresh));

        private DelegateCommand _MainViewShow;

        //主窗体是否通过托盘图标显示
        public DelegateCommand MainViewShow =>
            _MainViewShow ?? (_MainViewShow = new DelegateCommand(ExecuteMainViewShow));

        //主窗体是否显示
        private void ExecuteMainViewShow()
        {
            _eventAggregator.GetEvent<MainViewShow>().Publish();
            if (MainViewVisiblity == Visibility.Visible)
            {
                MainViewVisiblity = Visibility.Hidden;
                return;
            }
            else
            {
                MainViewVisiblity = Visibility.Visible;
                return;
            }
        }

        #endregion 命令方法

        #region 内部方法

        private void Test()
        {
            // db.SaveChanges();
        }

        private void Refresh()
        {
            db.SaveChanges();
            var ThingsLst = from Thing in db.Things where (Thing.Done == false) select Thing;

            Things.Clear();
            foreach (var item in ThingsLst)
            {
                Things.Add(item);
            }
        }

        private void SetTimer()
        {
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeSpan timeSpanMax = new TimeSpan(0, 0, 1);
            TimeSpan timeSpanMin = new TimeSpan(0, 0, 0);
            DateTime nowTime = DateTime.Now;
            var ThingsLst = from Thing in db.Things where (Thing.Done == false && Thing.Remind == true) select Thing;
            foreach (var thing in ThingsLst)
            {
                TimeSpan timeSpan = thing.RemindTime - nowTime;
                if (timeSpanMin < timeSpan && timeSpan < timeSpanMax)
                {
                    RemindView remindView = new RemindView();
                    remindView.Show();
                }
            }
        }

        #endregion 内部方法
    }
}