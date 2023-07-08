using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;

namespace Services.Services;

public static class Common
{
    /// <summary>
    ///     MD5转换
    /// </summary>
    /// <param name="str">源值</param>
    /// <returns>目标值</returns>
    public static string EncryptString(string str)
    {
        var md5 = MD5.Create();
        // 将字符串转换成字节数组
        var byteOld = Encoding.UTF8.GetBytes(str);
        // 调用加密方法
        var byteNew = md5.ComputeHash(byteOld);
        // 将加密结果转换为字符串
        var sb = new StringBuilder();
        foreach (var b in byteNew)
            // 将字节转换成16进制表示的字符串，
            sb.Append(b.ToString("x2"));
        // 返回加密的字符串
        return sb.ToString();
    }

    /// <summary>
    ///     延时调用方法
    /// </summary>
    /// <param name="time">延时时间S</param>
    /// <param name="eventHandler">被调用方法</param>
    public static void SetTimer(int time, ElapsedEventHandler eventHandler)
    {
        var timer = new Timer(time * 1000);
        timer.Elapsed += eventHandler;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    /// <summary>
    ///     判断字符串中是否包含中文
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool ContainChinese(string input)
    {
        var pattern = "[\u4e00-\u9fbb]";
        return Regex.IsMatch(input, pattern);
    }

    //时间戳函数
    public static long GetTimeStamp()
    {
        var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return (long)timeSpan.TotalSeconds;
    }
}