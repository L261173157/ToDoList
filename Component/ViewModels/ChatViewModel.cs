using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Component.Views;
using Database.Db;
using Prism.Commands;
using Prism.Mvvm;
using Services.Services;

namespace Component.ViewModels;

public class ChatViewModel : BindableBase
{
    public ChatViewModel()
    {
        TargetItemsSource = new ObservableCollection<string>(Enum.GetNames(typeof(TranslateTarget)));
        TbKeyUpEventCmd = new DelegateCommand<KeyEventArgs>(TbKeyUpEvent);
        ChatCmd = new DelegateCommand(Chat);
        DictOperateCmd = new DelegateCommand(ExecuteDictOperateCmd);
    }

    #region 属性定义

    private string _chatResult;

    /// <summary>
    ///     翻译文本框
    /// </summary>
    public string ChatResult
    {
        get => _chatResult;
        set => SetProperty(ref _chatResult, value);
    }

    /// <summary>
    ///     目标语言列表
    /// </summary>
    public ObservableCollection<string> TargetItemsSource { get; set; }

    private string _translateTarget;

    /// <summary>
    ///     目标语言
    /// </summary>
    public string TranslateTarget
    {
        get => _translateTarget;
        set => SetProperty(ref _translateTarget, value);
    }

    #endregion 属性定义

    #region 命令

    //翻译命令
    public DelegateCommand ChatCmd { get; }

    //字典操作命令
    public DelegateCommand DictOperateCmd { get; }

    //翻译命令
    public DelegateCommand<KeyEventArgs> TbKeyUpEventCmd { get; }

    #endregion 命令

    #region 内部方法

    private async void Chat()
    {
       ChatResult=await OpenAiApi.Chat(ChatResult);
    }

    //显示字典操作页面
    private void ExecuteDictOperateCmd()
    {
        var dictOperate = new ParameterView();
        dictOperate.Show();
    }

    //取消的方法，之后再试
    private void TbKeyUpEvent(KeyEventArgs eventArgs)
    {
        if (eventArgs.Key == Key.Enter) Chat();
    }

    #endregion 内部方法
}