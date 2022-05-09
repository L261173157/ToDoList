using System.Security.Cryptography;
using System.Text;
using System.Timers;

namespace Services.Services;

public static class Common
{
    //MD5转换
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

    //时间间隔
    public static void SetTimer(int time, ElapsedEventHandler eventHandler)
    {
        var timer = new Timer(time);
        timer.Elapsed += eventHandler;
        timer.AutoReset = true;
        timer.Enabled = true;
    }
}