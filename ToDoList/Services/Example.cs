using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Services;

namespace ToDoList.Services
{
    //示例服务
    public class Example : IExample
    {
        List<string> IExample.GetAll()
        {
            return new List<string>
            {
                "a",
                "b",
                "c"
            };
        }
    }
}
