using System.Collections.Generic;

namespace Services.Services.ClassType
{
    public class Weather
    {
        public string reason { get; set; }
        public Result result { get; set; }
        public int error_code { get; set; }
    }

    public class Realtime
    {
        public string temperature { get; set; }
        public string humidity { get; set; }
        public string info { get; set; }
        public string wid { get; set; }
        public string direct { get; set; }
        public string power { get; set; }
        public string aqi { get; set; }
    }

    public class Wid
    {
        public string day { get; set; }
        public string night { get; set; }
    }

    public class Future
    {
        public string date { get; set; }
        public string temperature { get; set; }
        public string weather { get; set; }
        public Wid wid { get; set; }
        public string direct { get; set; }
    }

    public class Result
    {
        public string city { get; set; }
        public Realtime realtime { get; set; }
        public List<Future> future { get; set; }
    }
}