﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Services.Services;

namespace Component.ViewModels
{
    public class WeatherViewModel : BindableBase
    {
        public WeatherViewModel()
        {
            WeatherQuery();

            Common.SetTimer(1800000, ((sender, args) => WeatherQuery()));
        }

        #region 属性定义

        private string weather;

        /// <summary>
        /// 天气文本框
        /// </summary>
        public string Weather
        {
            get { return weather; }
            set { SetProperty(ref weather, value); }
        }

        private string city;

        //查询城市
        public string City
        {
            get { return city; }
            set { SetProperty(ref city, value); }
        }

        #endregion 属性定义

        #region 命令

        private DelegateCommand _queryCmd;

        public DelegateCommand QueryCmd =>
            _queryCmd ??= new DelegateCommand(ExecuteQuery);

        private async void ExecuteQuery()
        {
            if (string.IsNullOrEmpty(City))
            {
                Weather = await WebApi.LocalWeather();
            }
            else
            {
                Weather = await WebApi.Weather(City);
            }
        }

        #endregion 命令

        #region 内部方法

        private async void WeatherQuery()
        {
            Weather = await WebApi.LocalWeather();
        }

        #endregion 内部方法
    }
}