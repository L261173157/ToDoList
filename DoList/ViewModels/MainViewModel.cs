using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DoList.Db;
using DoList.Models;
using DoList.Services.EventType;
using DoList.Views;
using Prism.Events;

namespace DoList.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IEventAggregator ea)

        {
            ea.GetEvent<MainViewRefresh>().Subscribe(Refresh);

            Things = new ObservableCollection<Thing>();
            _eventAggregator = ea;
            Refresh();
            RemindPast();
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
        private readonly ThingsContext db = new();

        /// <summary>
        /// 主界面数据列表
        /// </summary>
        public ObservableCollection<Thing> Things { get; set; }

        #endregion 属性定义

        #region 命令

        private DelegateCommand _newThingCmd;

        //弹出新建事务窗体
        public DelegateCommand NewThingViewCmd =>
            _newThingCmd ??= new DelegateCommand(ExecuteNewThingViewCmd);

        private void ExecuteNewThingViewCmd()
        {
            EditView editView = new EditView();
            editView.Show();

            _eventAggregator.GetEvent<EditViewTransmit>().Publish(null);
        }

        private DelegateCommand _SaveCmd;

        //保存命令
        public DelegateCommand SaveCmd =>
            _SaveCmd ?? (_SaveCmd = new DelegateCommand(ExecuteSaveCmd));

        private void ExecuteSaveCmd()
        {
            db.SaveChanges();
        }

        private DelegateCommand<string> _ShowDoneCmd;

        public DelegateCommand<string> ShowDoneCmd =>
            _ShowDoneCmd ?? (_ShowDoneCmd = new DelegateCommand<string>(ExecuteShowDoneCmd));

        void ExecuteShowDoneCmd(string nowStatus)
        {
            db.SaveChanges();
            switch (nowStatus)
            {
                case "全部":
                    var thingsLst = from thing in db.Things orderby (thing.Done) select thing;
                    Things.Clear();
                    foreach (var item in thingsLst)
                    {
                        Things.Add(item);
                    }
                    break;
                case "已完成":
                    var lst = from thing in db.Things where (thing.Done == false) select thing;

                    Things.Clear();
                    foreach (var item in lst)
                    {
                        Things.Add(item);
                    }
                    break;
                
            }
        }

        void ExecuteShowDoneCmd()
        {
            db.SaveChanges();
            var thingsLst = from thing in db.Things orderby (thing.Done) select thing;
            Things.Clear();
            foreach (var item in thingsLst)
            {
                Things.Add(item);
            }
        }

        #endregion 命令

        #region 内部方法

        private void Refresh()
        {
            db.SaveChanges();
            var thingsLst = from thing in db.Things where (thing.Done == false) select thing;

            Things.Clear();
            foreach (var item in thingsLst)
            {
                Things.Add(item);
            }

            RemindFuture();
        }

        #region 时间触发

        private void RemindFuture()
        {
            try
            {
                TimeSpan timeSpan;

                DateTime nowTime = DateTime.Now;
                Timers.Clear();
                var ThingsIQueryable = from Thing in db.Things
                    where (Thing.Done == false && Thing.Remind == true)
                    select Thing;

                foreach (var thing in ThingsIQueryable)
                {
                    timeSpan = thing.RemindTime - nowTime;
                    if (timeSpan >= TimeSpan.Zero)
                    {
                        Timer timer = new Timer(timeSpan.TotalSeconds * 1000);
                        timer.Elapsed += (sender, e) => Timer_Elapsed_Notify(thing);
                        timer.AutoReset = false;
                        timer.Enabled = true;
                        Timers.Add(timer);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //仅在启动时调用
        private void RemindPast()
        {
            try
            {
                DateTime nowTime = DateTime.Now;
                Timers.Clear();
                var thingsIQueryable = from Thing in db.Things
                    where (Thing.Done == false && Thing.Remind == true)
                    select Thing;

                foreach (var thing in thingsIQueryable)
                {
                    var timeSpan = thing.RemindTime - nowTime;
                    if (timeSpan < TimeSpan.Zero)
                    {
                        var timer = new Timer(2000);
                        timer.Elapsed += (sender, e) => Timer_Elapsed_Notify(thing);
                        timer.AutoReset = false;
                        timer.Enabled = true;
                        Timers.Add(timer);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Timer_Elapsed_Notify(Thing thing)
        {
            //发给mainview通知
            _eventAggregator.GetEvent<MainViewNotify>().Publish(thing);
        }

        #endregion 时间触发

        #endregion 内部方法
    }
}