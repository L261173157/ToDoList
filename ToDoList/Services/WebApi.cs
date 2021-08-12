using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ToDoList.Services.ClassType;
using Newtonsoft.Json;
using System.Web;

namespace ToDoList.Services
{
    public enum TranslateTarget
    {
        zh,
        en
    }

    public static class WebApi
    {
        private static readonly HttpClient client = new();

        /// <summary>
        /// 查询当地天气
        /// </summary>
        /// <returns></returns>
        public static async Task<string> LocalWeather()
        {
            string localWeather;
            string localCity;
            string localIP;
            try
            {
                localIP = await LocationIP();
                localCity = await Adress(localIP);
                localCity = localCity.Substring(0, localCity.Length - 1);
                localWeather = await Weather(localCity);
            }
            catch (Exception)
            {
                return "网络错误";
            }

            return localWeather;
        }

        /// <summary>
        /// 查询天气
        /// </summary>
        /// <param name="city">城市</param>
        /// <returns></returns>
        public static async Task<string> Weather(string city)
        {
            try
            {
                string url = "http://apis.juhe.cn/simpleWeather/query";
                string key = "abe434a6bb7e95827219b4ad24407816";
                string par = url + "?city=" + city + "&key=" + key;
                var response = await client.GetAsync(par);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Weather weather = JsonConvert.DeserializeObject<Weather>(responseBody);
                if (weather.error_code == 0)
                {
                    return weather.result.city + ":" + "\n" + weather.result.realtime.temperature + "°C," + "\n" +
                           weather.result.realtime.info + "\n" + weather.result.future[1].date + ":" + "\n" +
                           weather.result.future[1].temperature + "\n" + weather.result.future[1].weather;
                }
                else
                {
                    return "查询失败";
                }
            }
            catch (Exception)
            {
                return "查询失败";
            }
        }

        /// <summary>
        /// 查询本机公网IP
        /// </summary>
        /// <returns></returns>
        public static async Task<string> LocationIP()
        {
            try
            {
                string url = "http://httpbin.org/ip";

                string par = url;
                var response = await client.GetAsync(par);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                LocationIP locationIP = JsonConvert.DeserializeObject<LocationIP>(responseBody);
                return locationIP.origin;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 查询城市地址
        /// </summary>
        /// <param name="ip">IP</param>
        /// <returns></returns>
        public static async Task<string> Adress(string ip)
        {
            try
            {
                string url = "http://apis.juhe.cn/ip/ipNew";
                string key = "ddd8a4dc286bdc58a75fd47fda75ff0c";
                string par = url + "?ip=" + ip + "&key=" + key;
                var response = await client.GetAsync(par);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Adress adress = JsonConvert.DeserializeObject<Adress>(responseBody);
                if (adress.error_code == 0)
                {
                    return adress.result.City;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task<string> Translate(string query, TranslateTarget target)
        {
            string to;
            try
            {
                string q;
                if (string.IsNullOrEmpty(query))
                {
                    return "请输入内容";
                }
                else
                {
                    q = query;
                }
                // 原文

                // 源语言
                string from = "auto";
                // 目标语言

                switch (target)
                {
                    case TranslateTarget.zh:
                        to = "zh";
                        break;

                    case TranslateTarget.en:
                        to = "en";
                        break;

                    default:
                        to = "zh";
                        break;
                }

                // 改成您的APP ID
                string appId = "20210730000901668";
                Random rd = new Random();
                string salt = rd.Next(100000).ToString();
                // 改成您的密钥
                string secretKey = "gOLq8gchFBxy0JA_pBCu";
                string sign = Common.EncryptString(appId + q + salt + secretKey);
                string url = "http://api.fanyi.baidu.com/api/trans/vip/translate?";
                url += "q=" + HttpUtility.UrlEncode(q);
                url += "&from=" + from;
                url += "&to=" + to;
                url += "&appid=" + appId;
                url += "&salt=" + salt;
                url += "&sign=" + sign;
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Translate result = JsonConvert.DeserializeObject<Translate>(responseBody);

                return result.trans_result[0].dst;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}