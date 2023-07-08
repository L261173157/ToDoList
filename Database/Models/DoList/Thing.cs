using System;
using Prism.Mvvm;

namespace Database.Models.DoList;

public class Thing : BindableBase
{
    private string content;

    private DateTime createTime;

    private bool done;

    private DateTime finishTime;

    private bool remind;

    private DateTime remindTime;
    private int thingId;

    /// <summary>
    ///     ID
    /// </summary>
    public int ThingId
    {
        get => thingId;
        set => SetProperty(ref thingId, value);
    }

    /// <summary>
    ///     内容
    /// </summary>
    public string Content
    {
        get => content;
        set => SetProperty(ref content, value);
    }

    /// <summary>
    ///     是否完成
    /// </summary>
    public bool Done
    {
        get => done;
        set => SetProperty(ref done, value);
    }

    /// <summary>
    ///     创建时间
    /// </summary>
    public DateTime CreateTime
    {
        get => createTime;
        set => SetProperty(ref createTime, value);
    }

    /// <summary>
    ///     完成时间
    /// </summary>
    public DateTime FinishedTime
    {
        get => finishTime;
        set => SetProperty(ref finishTime, value);
    }

    /// <summary>
    ///     是否提醒
    /// </summary>
    public bool Remind
    {
        get => remind;
        set => SetProperty(ref remind, value);
    }

    /// <summary>
    ///     提醒时间
    /// </summary>
    public DateTime RemindTime
    {
        get => remindTime;
        set => SetProperty(ref remindTime, value);
    }

    private long _createTimeStamp;

    /// <summary>
    ///     创建时间戳
    /// </summary>
    public long CreateTimeStamp
    {
        get => _createTimeStamp;
        set => SetProperty(ref _createTimeStamp, value);
    }

    private long _updateTimeStamp;

    /// <summary>
    ///     更新时间戳
    /// </summary>
    public long UpdateTimeStamp
    {
        get => _updateTimeStamp;
        set => SetProperty(ref _updateTimeStamp, value);
    }
}