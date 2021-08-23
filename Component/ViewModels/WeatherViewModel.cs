using Prism.Commands;
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

        #endregion

        #region 内部方法

        private async void WeatherQuery()
        {
            Weather = await WebApi.LocalWeather();
        }

        #endregion
    }
}