using System.Collections.Generic;

namespace Services.Services.ClassType
{
    public class Translate
    {
        public string from { get; set; }
        public string to { get; set; }
        public List<TransResult> trans_result { get; set; }
    }

    public class TransResult
    {
        public string src { get; set; }
        public string dst { get; set; }
    }
}