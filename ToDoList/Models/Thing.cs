using Prism.Mvvm;
using System;

namespace ToDoList.Models
{
    public class Thing : BindableBase
    {
        private int thingId;

        public int ThingId
        {
            get { return thingId; }
            set { SetProperty(ref thingId, value); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        private bool done;

        public bool Done
        {
            get { return done; }
            set { SetProperty(ref done, value); }
        }

        private DateTime creatTime;

        public DateTime CreatTime
        {
            get { return creatTime; }
            set { SetProperty(ref creatTime, value); }
        }

        private DateTime finishTime;

        public DateTime FinishedTime
        {
            get { return finishTime; }
            set { SetProperty(ref finishTime, value); }
        }
    }
}