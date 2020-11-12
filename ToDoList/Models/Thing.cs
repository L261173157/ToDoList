using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoList.Models
{
   public class Thing
    {
        public int ThingId { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Done { get; set; }
        public DateTime CreatTime { get; set; }
        public DateTime FinishedTime { get; set; }

    }
}
