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

public class TranslateViewModel : BindableBase
{
    public TranslateViewModel()
    {
        TargetItemsSource = new ObservableCollection<string>(Enum.GetNames(typeof(TranslateTarget)));
        TbKeyUpEventCmd = new DelegateCommand<KeyEventArgs>(TbKeyUpEvent);
        TranslateCmd = new DelegateCommand(TranslateQuery);
        ParameterCmd = new DelegateCommand(ExecuteParameterCmd);
    }

    #region 属性定义

    private string _translateResult;

    /// <summary>
    ///     翻译文本框
    /// </summary>
    public string TranslateResult
    {
        get => _translateResult;
        set => SetProperty(ref _translateResult, value);
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
    public DelegateCommand TranslateCmd { get; }

    //字典操作命令
    public DelegateCommand ParameterCmd { get; }

    //翻译命令
    public DelegateCommand<KeyEventArgs> TbKeyUpEventCmd { get; }

    #endregion 命令

    #region 内部方法

    private async void TranslateQuery()
    {
        TranslateTarget target;
        switch (TranslateTarget)
        {
            case "zh":
                target = Services.Services.TranslateTarget.zh;
                break;

            case "en":
                target = Services.Services.TranslateTarget.en;
                break;

            default:
                target = Services.Services.TranslateTarget.zh;
                break;
        }

        await using (var context = new ContextLocal())
        {
            try
            {
                TranslateResult = Regex.Replace(TranslateResult, @"\s+", "").ToLower();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                return;
            }

            if (Common.ContainChinese(TranslateResult))
            {
                target = Services.Services.TranslateTarget.en;
                TranslateResult = await WebApi.Translate(TranslateResult, target);
                return;
            }

            var result = (from dict in context.DictDbs where dict.Word == TranslateResult select dict.Translation)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(result))
            {
                result = result.Replace("\\n", Environment.NewLine);
                TranslateResult = result;
            }

            else
            {
                TranslateResult = await WebApi.Translate(TranslateResult, target);
            }
        }
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
        if (eventArgs.Key == Key.Enter) TranslateQuery();
    }

    #endregion 内部方法
}