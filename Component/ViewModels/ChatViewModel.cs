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
            new ObservableCollection<string>(new string[] { "通用", ".net专家", "英翻中", "中翻英" });

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

    //参数命令
    public DelegateCommand ParameterCmd { get; }

    #endregion 命令

    #region 内部方法

    private async void Chat()
    {
        string systemInput = chatRole.通用;
        switch (SystemInput)
        {
            case "通用":
                systemInput = chatRole.通用;
                break;
            case "英翻中":
                systemInput = chatRole.英翻中;
                break;
            case "中翻英":
                systemInput = chatRole.中翻英;
                break;
            case ".net专家":
                systemInput = chatRole.dotnet专家;
                break;
        }

//TODO 未来需要修改catch内容
        try
        {
            ChatResult = await OpenAiApi.Chat(ChatResult, systemInput);
        }
        catch (Exception e)
        {
            ChatResult = e.Message;
        }
    }

    //显示参数页面
    private void ExecuteParameterCmd()
    {
        var parameterView = new ParameterView();
        parameterView.Show();
    }

    #endregion 内部方法
}