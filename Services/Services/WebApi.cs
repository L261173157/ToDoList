using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Services.Services.ClassType;

namespace Services.Services;

public enum TranslateTarget
{
    zh,
    en
}

public static class WebApi
{
    private static readonly HttpClient client = new();

    /// <summary>
    ///     查询当地天气
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
            localCity = await Address(localIP);
            if (localCity.EndsWith("市"))
                localCity = localCity.Substring(0, localCity.Length - 1);
            localWeather = await Weather(localCity);
            return localWeather;
        }
        catch (Exception)
        {
            return "网络错误";
        }
    }

    /// <summary>
    ///     查询天气
    /// </summary>
    /// <param name="city">城市</param>
    /// <returns></returns>
    public static async Task<string> Weather(string city)
    {
        try
        {
            var url = "http://apis.juhe.cn/simpleWeather/query";
            var key = ConfigurationManager.AppSettings["weatherKey"];
            if (city.EndsWith("市"))
                city = city.Substring(0, city.Length - 1);
            var par = url + "?city=" + city + "&key=" + key;
            var response = await client.GetAsync(par);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            //通过jsonconvert接收api信息，转化为weather
            var weather = JsonConvert.DeserializeObject<Weather>(responseBody);
            if (weather.error_code == 0)
                return weather.result.city + ":" + "\n" + weather.result.realtime.temperature + "°C," + "\n" +
                       weather.result.realtime.info + "\n" + weather.result.future[1].date + ":" + "\n" +
                       weather.result.future[1].temperature + "\n" + weather.result.future[1].weather;
            return "查询失败";
        }
        catch (Exception)
        {
            return "查询失败";
        }
    }

    /// <summary>
    ///     查询本机公网IP
    /// </summary>
    /// <returns></returns>
    public static async Task<string> LocationIP()
    {
        var url = "http://httpbin.org/ip";

        var par = url;
        var response = await client.GetAsync(par);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        var locationIP = JsonConvert.DeserializeObject<LocationIP>(responseBody);
        return locationIP.origin;
    }

    /// <summary>
    ///     查询城市地址
    /// </summary>
    /// <param name="ip">IP</param>
    /// <returns></returns>
    public static async Task<string> Address(string ip)
    {
        var url = "http://apis.juhe.cn/ip/ipNew";
        var key = ConfigurationManager.AppSettings["addressKey"];
        var par = url + "?ip=" + ip + "&key=" + key;
        var response = await client.GetAsync(par);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();

        var adress = JsonConvert.DeserializeObject<Adress>(responseBody);
        if (adress.error_code == 0)
            if (adress.result.City == "")
                return adress.result.Province;
            else
                return adress.result.City;

        return "";
    }

    /// <summary>
    ///     翻译
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static async Task<string> Translate(string query, TranslateTarget target)
    {
        string to;
        string q;
        if (string.IsNullOrEmpty(query))
            return "请输入内容";
        q = query;
        // 原文

        // 源语言
        var from = "auto";
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

        // 改成自己的APP ID
        var appId = ConfigurationManager.AppSettings["translateAppId"];
        var rd = new Random();
        var salt = rd.Next(100000).ToString();
        // 改成您的密钥
        var secretKey = ConfigurationManager.AppSettings["translateSecretKey"];
        var sign = Common.EncryptString(appId + q + salt + secretKey);
        var url = "http://api.fanyi.baidu.com/api/trans/vip/translate?";
        url += "q=" + HttpUtility.UrlEncode(q);
        url += "&from=" + from;
        url += "&to=" + to;
        url += "&appid=" + appId;
        url += "&salt=" + salt;
        url += "&sign=" + sign;

        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Translate>(responseBody);
            return result.trans_result[0].dst;
        }
        catch (Exception e)
        {
            return "翻译失败" + Environment.NewLine + e.Message;
        }
    }
}