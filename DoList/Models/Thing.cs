using System;
using Prism.Mvvm;

namespace DoList.Models
{
    public class Thing : BindableBase
    {
        private int thingId;
        /// <summary>
        /// ID
        /// </summary>
        public int ThingId
        {
            get { return thingId; }
            set { SetProperty(ref thingId, value); }
        }

        private string content;
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        private bool done;
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool Done
        {
            get { return done; }
            set { SetProperty(ref done, value); }
        }

        private DateTime creatTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime
        {
            get { return creatTime; }
            set { SetProperty(ref creatTime, value); }
        }

        private DateTime finishTime;
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime FinishedTime
        {
            get { return finishTime; }
            set { SetProperty(ref finishTime, value); }
        }

        private bool remind;
        /// <summary>
        /// 是否提醒
        /// </summary>
        public bool Remind
        {
            get { return remind; }
            set { SetProperty(ref remind, value); }
        }

        private DateTime remindTime;
        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime RemindTime
        {
            get { return remindTime; }
            set { SetProperty(ref remindTime, value); }
        }
    }
}