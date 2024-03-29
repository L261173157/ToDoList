﻿using System;
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
        TranslateCmd = new DelegateCommand(TranslateQuery);
        ParameterCmd = new DelegateCommand(ExecuteParameterCmd);
    }

    #region 属性定义

    private string _translate;

    /// <summary>
    ///     翻译目标文本框
    /// </summary>
    public string Translate
    {
        get => _translate;
        set => SetProperty(ref _translate, value);
    }

    private string _translateResult;

    /// <summary>
    ///     翻译结果文本框
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
                Translate = Regex.Replace(Translate, @"\s+", "").ToLower();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                return;
            }

            try
            {
                if (Common.ContainChinese(Translate))
                {
                    target = Services.Services.TranslateTarget.en;
                    TranslateResult = await WebApi.Translate(Translate, target);
                    return;
                }

                var result = (from dict in context.DictDbs where dict.Word == Translate select dict.Translation)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Replace("\\n", Environment.NewLine);
                    TranslateResult = result;
                }
                else
                {
                    TranslateResult = await WebApi.Translate(Translate, target);
                }
            }
            catch (Exception e)
            {
                TranslateResult = e.Message;
            }
        }
    }

    //显示字典操作页面
    private void ExecuteParameterCmd()
    {
        var parameterView = new ParameterView();
        parameterView.Show();
    }

    #endregion 内部方法
}