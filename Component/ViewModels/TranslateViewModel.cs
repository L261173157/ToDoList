﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using Services.Services;
using Services.Services.ClassType;

namespace Component.ViewModels
{
    public class TranslateViewModel : BindableBase
    {
        public TranslateViewModel()
        {
            TargetItemsSource = new ObservableCollection<string>(Enum.GetNames(typeof(TranslateTarget)));
        }

        #region 属性定义
        private string _translateResult;

        /// <summary>
        /// 翻译文本框
        /// </summary>
        public string TranslateResult
        {
            get { return _translateResult; }
            set { SetProperty(ref _translateResult, value); }
        }

        /// <summary>
        /// 目标语言列表
        /// </summary>
        public ObservableCollection<string> TargetItemsSource { get; set; }

        private string _translateTarget;

        /// <summary>
        /// 目标语言
        /// </summary>
        public string TranslateTarget
        {
            get { return _translateTarget; }
            set { SetProperty(ref _translateTarget, value); }
        }


        #endregion

        #region 命令

        private DelegateCommand _translateCmd;

        //翻译命令
        public DelegateCommand TranslateCmd =>
            _translateCmd ??= new DelegateCommand(TranslateQuery, CanTranslateQuery);

        #endregion

        #region 内部方法

        private bool CanTranslateQuery()
        {
            return true;
          //  return !string.IsNullOrEmpty(TranslateResult);
        }

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

            TranslateResult = await WebApi.Translate(TranslateResult, target);
            //把文字翻译为目标语言
        }

        #endregion

    }
}