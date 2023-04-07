using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Component.Views;
using Prism.Commands;
using Prism.Mvvm;
using Services;
using Services.Services;

namespace Component.ViewModels;

public class ChatViewModel : BindableBase
{
    public ChatViewModel()
    {
        SystemInputSource =
            new ObservableCollection<string>(new string[] { "通用", "Linux_终端", "英翻中", "英英词典", "英语翻译和改进者" });
        TbKeyUpEventCmd = new DelegateCommand<KeyEventArgs>(TbKeyUpEvent);
        ChatCmd = new DelegateCommand(Chat);
        ParameterCmd = new DelegateCommand(ExecuteParameterCmd);
    }

    #region 属性定义

    private string _chatResult;

    /// <summary>
    ///     问答文本框
    /// </summary>
    public string ChatResult
    {
        get => _chatResult;
        set => SetProperty(ref _chatResult, value);
    }

    private string assistantInput;

    /// <summary>
    ///     列表
    /// </summary>
    public ObservableCollection<string> SystemInputSource { get; set; }


    private string _systemInput;

    /// <summary>
    ///     系统输入
    /// </summary>
    public string SystemInput
    {
        get => _systemInput;
        set => SetProperty(ref _systemInput, value);
    }

    #endregion 属性定义

    #region 命令

    //绑定界面问答命令
    public DelegateCommand ChatCmd { get; }

    //字典操作命令
    public DelegateCommand ParameterCmd { get; }

    //翻译命令
    public DelegateCommand<KeyEventArgs> TbKeyUpEventCmd { get; }

    #endregion 命令

    #region 内部方法

    private async void Chat()
    {
        string systemInput=chatRole.通用;
        switch (SystemInput)
        {
            case "通用":
                systemInput = chatRole.通用;
                break;
            case "Linux 终端":
                systemInput = chatRole.Linux_终端;
                break;
            case "英翻中":
                systemInput = chatRole.英翻中;
                break;
            case "英英词典":
                systemInput = chatRole.英英词典;
                break;
            case "英语翻译和改进者":
                systemInput = chatRole.英语翻译和改进者;
                break;
        }

        ChatResult = await OpenAiApi.Chat(ChatResult,systemInput);
        //assistantInput = ChatResult;
    }

    //显示字典操作页面
    private void ExecuteParameterCmd()
    {
        var parameterView = new ParameterView();
        parameterView.Show();
    }

    //取消的方法，之后再试
    private void TbKeyUpEvent(KeyEventArgs eventArgs)
    {
        if (eventArgs.Key == Key.Enter) Chat();
    }

    #endregion 内部方法
}