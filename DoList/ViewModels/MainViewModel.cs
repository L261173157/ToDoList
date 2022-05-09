using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using Database.Db;
using Database.Models.DoList;
using DoList.Services.EventType;
using DoList.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace DoList.ViewModels;

public class MainViewModel : BindableBase
{
    public MainViewModel(IEventAggregator ea)

    {
        ea.GetEvent<MainViewRefresh>().Subscribe(Refresh);

        Things = new ObservableCollection<Thing>();
        _eventAggregator = ea;
        NowStatus = "未完成";
        Refresh();
        RemindPast();
    }

    #region 属性定义

    /// <summary>
    ///     提醒功能时间间隔组
    /// </summary>
    private readonly List<Timer> _timers = new();

    /// <summary>
    ///     事件聚合器
    /// </summary>
    private readonly IEventAggregator _eventAggregator;

    /// <summary>
    ///     数据库
    /// </summary>
    private readonly Context _db = new();

    /// <summary>
    ///     主界面数据列表
    /// </summary>
    public ObservableCollection<Thing> Things { get; set; }

    private string _nowStatus;

    public string NowStatus
    {
        get => _nowStatus;
        set => SetProperty(ref _nowStatus, value);
    }

    #endregion 属性定义

    #region 命令

    private DelegateCommand _newThingCmd;

    //弹出新建事务窗体
    public DelegateCommand NewThingViewCmd =>
        _newThingCmd ??= new DelegateCommand(ExecuteNewThingViewCmd);

    private void ExecuteNewThingViewCmd()
    {
        var editView = new EditView();
        editView.Show();

        _eventAggregator.GetEvent<EditViewTransmit>().Publish(null);
    }

    private DelegateCommand _saveCmd;

    //保存命令
    public DelegateCommand SaveCmd =>
        _saveCmd ??= new DelegateCommand(ExecuteSaveCmd);

    private void ExecuteSaveCmd()
    {
        Refresh();
    }

    private DelegateCommand _showNowStatusCmd;

    //显示当前状态
    public DelegateCommand ShowNowStatusCmd =>
        _showNowStatusCmd ??= new DelegateCommand(ExecuteShowNowStatusCmd);

    private void ExecuteShowNowStatusCmd()
    {
        switch (NowStatus)
        {
            case "全部":

                NowStatus = "未完成";
                break;

            case "未完成":

                NowStatus = "全部";
                break;
        }

        Refresh();
    }

    #endregion 命令

    #region 内部方法

    //刷新
    private void Refresh()
    {
        _db.SaveChanges();
        switch (NowStatus)
        {
            case "全部":
                var thingsLst = from thing in _db.Things orderby thing.Done select thing;
                Things.Clear();
                foreach (var item in thingsLst) Things.Add(item);

                break;

            case "未完成":
                var lst = from thing in _db.Things where thing.Done == false select thing;

                Things.Clear();
                foreach (var item in lst) Things.Add(item);

                break;
        }

        RemindFuture();
    }

    #region 时间触发

    private void RemindFuture()
    {
        TimeSpan timeSpan;

        var nowTime = DateTime.Now;
        _timers.Clear();
        var thingsIQueryable = from thing in _db.Things
            where thing.Done == false && thing.Remind == true
            select thing;

        foreach (var thing in thingsIQueryable)
        {
            timeSpan = thing.RemindTime - nowTime;
            if (timeSpan >= TimeSpan.Zero)
            {
                var timer = new Timer(timeSpan.TotalSeconds * 1000);
                timer.Elapsed += (sender, e) => Timer_Elapsed_Notify(thing);
                timer.AutoReset = false;
                timer.Enabled = true;
                _timers.Add(timer);
            }
        }
    }

    //仅在启动时调用
    private void RemindPast()
    {
        var nowTime = DateTime.Now;
        _timers.Clear();
        var thingsIQueryable = from thing in _db.Things
            where thing.Done == false && thing.Remind == true
            select thing;

        foreach (var thing in thingsIQueryable)
        {
            var timeSpan = thing.RemindTime - nowTime;
            if (timeSpan < TimeSpan.Zero)
            {
                var timer = new Timer(2000);
                timer.Elapsed += (sender, e) => Timer_Elapsed_Notify(thing);
                timer.AutoReset = false;
                timer.Enabled = true;
                _timers.Add(timer);
            }
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