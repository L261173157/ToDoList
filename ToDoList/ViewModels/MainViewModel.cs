using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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
            // SetTimer();
        }

        #region 属性定义

        /// <summary>
        /// 提醒功能时间间隔组
        /// </summary>
        private List<Timer> Timers = new List<Timer>();

        /// <summary>
        /// 事件聚合器
        /// </summary>
        private IEventAggregator _eventAggregator;

        /// <summary>
        /// 数据库
        /// </summary>
        private readonly ThingsContext db = new ThingsContext();

        /// <summary>
        /// 主界面数据列表
        /// </summary>
        public ObservableCollection<Thing> Things { get; set; }

        private Visibility mainViewVisiblity = Visibility.Visible;

        /// <summary>
        /// 主界面隐藏属性
        /// </summary>
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
            EditView editView = new EditView();
            editView.Show();

            _eventAggregator.GetEvent<EditViewTransmit>().Publish(null);
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
            Timer timer = new Timer(5000);
            timer.Elapsed += (sender, e) => Timer_Elapsed(new Thing());
            timer.AutoReset = false;
            timer.Enabled = true;

            //Timer_Elapsed( new Thing());
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
            Remind();
        }

        #region 时间触发

        private void Remind()
        {
            try
            {
                TimeSpan timeSpan;
               
                DateTime nowTime = DateTime.Now;
                Timers.Clear();
                var ThingsIQueryable = from Thing in db.Things where (Thing.Done == false && Thing.Remind == true && Thing.RemindTime > nowTime) select Thing;

                foreach (var thing in ThingsIQueryable)
                {
                    timeSpan = thing.RemindTime - nowTime;

                    Timer timer = new Timer(timeSpan.TotalSeconds * 1000);
                    timer.Elapsed += (sender, e) => Timer_Elapsed(thing);
                    timer.AutoReset = false;
                    timer.Enabled = true;
                    Timers.Add(timer);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Timer_Elapsed(Thing thing)
        {
            try
            {
                //由于Timer另外开启线程，使用下列代码
                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
                {
                    EditView editView = new EditView();
                    editView.Show();
                    _eventAggregator.GetEvent<EditViewTransmit>().Publish(thing);
                }
                  ));
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        #endregion 时间触发

        #endregion 内部方法
    }
}