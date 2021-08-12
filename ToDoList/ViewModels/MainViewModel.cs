using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using ToDoList.Db;
using ToDoList.Models;
using ToDoList.Services.EventType;
using ToDoList.Views;
using ToDoList.Services;
using System.Threading.Tasks;
using System.Windows;

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
            RemindPast();
            WeatherQuery();
            TargetItemsSource = new ObservableCollection<string>(Enum.GetNames(typeof(TranslateTarget)));
            // SetTimer();
        }

        #region 属性定义

        private string translate;

        /// <summary>
        /// 翻译文本框
        /// </summary>
        public string Translate
        {
            get { return translate; }
            set { SetProperty(ref translate, value); }
        }

        /// <summary>
        /// 目标语言列表
        /// </summary>
        public ObservableCollection<string> TargetItemsSource { get; set; }

        private string target;

        /// <summary>
        /// 目标语言
        /// </summary>
        public string Target
        {
            get { return target; }
            set { SetProperty(ref target, value); }
        }

        private string weather;

        /// <summary>
        /// 天气文本框
        /// </summary>
        public string Weather
        {
            get { return weather; }
            set { SetProperty(ref weather, value); }
        }

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

        private DelegateCommand _testCmd;

        public DelegateCommand TestCmd =>
            _testCmd ?? (_testCmd = new DelegateCommand(Test));

        private DelegateCommand _RefreshCmd;

        //保存命令
        public DelegateCommand RefreshCmd =>
            _RefreshCmd ?? (_RefreshCmd = new DelegateCommand(Refresh));

        private DelegateCommand _translateCmd;

        //翻译命令
        public DelegateCommand TranslateCmd =>
            _translateCmd ??= new DelegateCommand(TranslateQuery, CanTranslateQuery);

        #endregion 命令

        #region 内部方法

        private async void Test()
        {
            // Timer_Elapsed_Notify(new Thing() { Content = "test" });
            WeatherQuery();
            TranslateQuery();
        }

        //检查是否能够执行翻译
        private bool CanTranslateQuery()
        {
            return true;
            // return !string.IsNullOrEmpty(Translate);
        }

        private async void TranslateQuery()
        {
            TranslateTarget target;
            switch (Target)
            {
                case "zh":
                    target = TranslateTarget.zh;
                    break;

                case "en":
                    target = TranslateTarget.en;
                    break;

                default:
                    target = TranslateTarget.zh;
                    break;
            }

            Translate = await WebApi.Translate(Translate, target);
            //把文字翻译为目标语言
        }

        /// <summary>
        /// 天气查询
        /// </summary>
        private async void WeatherQuery()
        {
            Weather = await WebApi.LocalWeather();
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
                var ThingsIQueryable = from Thing in db.Things where (Thing.Done == false && Thing.Remind == true) select Thing;

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
                TimeSpan timeSpan;

                DateTime nowTime = DateTime.Now;
                Timers.Clear();
                var ThingsIQueryable = from Thing in db.Things where (Thing.Done == false && Thing.Remind == true) select Thing;

                foreach (var thing in ThingsIQueryable)
                {
                    timeSpan = thing.RemindTime - nowTime;
                    if (timeSpan < TimeSpan.Zero)
                    {
                        Timer timer = new Timer(2000);
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

        //新增一行
    }
}