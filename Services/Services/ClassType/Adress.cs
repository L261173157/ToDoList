namespace Services.Services.ClassType
{
   public class Adress
    {
        public string resultcode { get; set; }
        public string reason { get; set; }
        public Result1 result { get; set; }
        public int error_code { get; set; }
    }
    public class Result1
    {
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Isp { get; set; }
    }
}
