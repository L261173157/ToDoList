using System;
using System.Linq;
using Database.Db;
using Database.Models.DoList;
using DoList.Services.EventType;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace DoList.ViewModels
{
    public class EditViewModel : BindableBase
    {
        public EditViewModel(IEventAggregator ea)
        {
            _eventAggregator = ea;
            ea.GetEvent<EditViewTransmit>().Subscribe(Transmit, ThreadOption.UIThread);
        }

        #region 属性定义

        private IEventAggregator _eventAggregator;
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

        #endregion 属性定义

        #region 命令

        private DelegateCommand _saveCmd;

        public DelegateCommand SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand(ExecuteSaveCmd));

        private void ExecuteSaveCmd()
        {
            using (var db = new Context())
            {
                if (ThingId == 0)
                {
                    db.Things.Add(new Thing
                    {
                        Content = this.Content, CreatTime = DateTime.Now, Remind = this.Remind,
                        RemindTime = this.RemindTime
                    });
                }
                else
                {
                    var thingNeedChang = db.Things.Single(b => b.ThingId == this.ThingId);
                    thingNeedChang.Content = this.Content;
                    thingNeedChang.Done = this.Done;
                    thingNeedChang.Remind = this.Remind;
                    thingNeedChang.RemindTime = this.RemindTime;
                }

                db.SaveChanges();
            }

            _eventAggregator.GetEvent<MainViewRefresh>().Publish();
        }

        #endregion 命令

        #region 内部方法

        public void Transmit(Thing thing)
        {
            if (thing != null)
            {
                ThingId = thing.ThingId;
                Content = thing.Content;
                Remind = false;
                RemindTime = thing.RemindTime;
            }
            else
            {
                ThingId = 0;
                RemindTime = DateTime.Now;
            }
        }

        #endregion 内部方法
    }
}